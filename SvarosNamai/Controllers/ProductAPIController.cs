using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SvarosNamai.Service.ProductAPI.Data;
using SvarosNamai.Service.ProductAPI.Models;
using SvarosNamai.Service.ProductAPI.Models.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvarosNamai.Service.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        protected ResponseDto _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext db, IMapper mapper) 
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet("GetBundles")]
        public ResponseDto GetBundles()
        {
            _response.Result = _mapper.Map<IEnumerable<Bundle>>(_db.Bundles.ToList());
            return _response;
        }

        [HttpGet("GetProducts")]
        public ResponseDto GetProducts()
        {
            _response.Result = _mapper.Map<IEnumerable<Product>>(_db.Products.ToList());
            return _response;
        }

        [HttpGet("GetBundle/{bundleId}")]
        public ResponseDto GetBundle(int bundleId)
        {
            try
            {
                Bundle bundle = _db.Bundles.FirstOrDefault(u => u.BundleId == bundleId);
                if(bundle != null) 
                {
                    _response.Result = bundle;
                    _response.Message = "Successfully Retrieved";
                    return _response;
                }
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetProduct/{productId}")]
        public ResponseDto GetProduct(int productId)
        {
            try
            {
                Product product = _db.Products.FirstOrDefault(u => u.ProductId == productId);
                if (product != null)
                {
                    _response.Result = product;
                    _response.Message = "Successfully Retrieved";
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("AddBundle")]
        public async Task<ResponseDto> AddBundle(BundleDto bundle)
        {
            try
            {
                var bundleNameFromDb = await _db.Bundles.AsNoTracking().FirstOrDefaultAsync(u => u.BundleName.ToUpper() == bundle.BundleName.ToUpper());
                if (bundleNameFromDb == null) 
                {
                    Bundle bundleToDb = _mapper.Map<Bundle>(bundle);
                    bundleToDb.DateCreated = DateTime.Now;
                    _db.Bundles.Add(bundleToDb);
                    await _db.SaveChangesAsync();
                    _response.Message = "Added Successfully";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bundle already exists";
                }
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;    
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("AddProduct")]
        public async Task<ResponseDto> AddProduct(ProductDto product)
        {
            try
            {
                var productNameFromDb = await _db.Products.AsNoTracking().FirstOrDefaultAsync(u => u.Name.ToUpper() == product.Name.ToUpper());
                if (productNameFromDb == null)
                {
                    Product productToDb = _mapper.Map<Product>(product);
                    productToDb.DateCreated = DateTime.Now;
                    _db.Products.Add(productToDb);
                    await _db.SaveChangesAsync();
                    _response.Message = "Added Successfully";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product already exists";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("AddProductToBundle")]
        public async Task<ResponseDto> AddProductToBundle(ProductBundleDto productBundle)
        {
            try
            {
                var bundleIdCheck =  await _db.Bundles.FirstOrDefaultAsync(u => u.BundleId == productBundle.BundleId);
                if (bundleIdCheck != null)
                {

                    foreach (var produkt in productBundle.ProductIds)
                    {
                        var productCheck = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == produkt);
                        if (productCheck != null)
                        {
                            if (!_db.ProductBundle.Any(u => u.ProductId == produkt && u.BundleId == productBundle.BundleId))
                            {
                                ProductBundle bundleToDb = new ProductBundle()
                                {
                                    BundleId = productBundle.BundleId,
                                    ProductId = produkt
                                };
                                _db.ProductBundle.Add(bundleToDb);
                            }
                        }
                        else
                        {
                            _response.Message = $"Product with the id {produkt} does not exist";
                            break;
                        }
                    }
                    await _db.SaveChangesAsync();
                }
                else
                {
                    _response.Message = "Bundle does not exist";
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
