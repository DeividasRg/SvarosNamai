﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SvarosNamai.Serivce.ProductAPI.Service.IService;
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
        private IErrorLogger _error;

        public ProductAPIController(AppDbContext db, IMapper mapper, IErrorLogger error) 
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _error = error;
        }

        [HttpGet("GetProducts")]
        public ResponseDto GetProducts()
        {
            _response.Result = _mapper.Map<IEnumerable<Product>>(_db.Products.ToList());
            return _response;
        }

        [HttpGet("GetAllActiveBundles")]
        public ResponseDto GetAllActiveBundles()
        {
            try
            {
                IEnumerable<Bundle> bundlesFromDB = _db.Bundles.Where(u => u.IsActive == true);
                if (bundlesFromDB != null)
                {
                    IEnumerable<BundleDto> bundleDtosFromDb = _mapper.Map<IEnumerable<BundleDto>>(bundlesFromDB);
                    _response.Result = bundleDtosFromDb;
                }
                else
                {
                    throw new Exception("No active bundles");
                }
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }

        [HttpGet("GetBundle/{bundleId}")]
        public ResponseDto GetBundle(int bundleId)
        {
            try
            {
                BundleDto bundle = _mapper.Map<BundleDto>(_db.Bundles.Find(bundleId));
                if (bundle != null)
                {
                    _response.Result = bundle;
                    _response.Message = "Successfully Retrieved";
                    return _response;
                }
                else
                {
                    throw new Exception("Bundle doesn't exist");
                }
            }


            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError (_response.Message);    
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
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError (_response.Message);    
            }
            return _response;
        }

        [HttpGet("GetProductByName/{productName}")]
        public ResponseDto GetProductByName(string productName)
        {
            try
            {
                Product product = _db.Products.FirstOrDefault(u => u.Name.ToLower() == productName.ToLower());
                if (product != null)
                {
                    _response.Result = product;
                    _response.Message = "Successfully Retrieved";
                    return _response;
                }
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }

        [Authorize]
        [HttpPost("AddBundle")]
        public async Task<ResponseDto> AddBundle(BundleToAddDto bundle)
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
                    throw new Exception("Bundle already exists");
                }
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;    
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }
        [Authorize]
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
                    throw new Exception("Product alredy exists");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }
        

        [Authorize]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ResponseDto> DeleteProduct(int id)
        {
            try
            {
                var check = await _db.Products.FindAsync(id);
                if (check != null)
                {
                        _db.Products.Remove(check);
                        _db.SaveChanges();
                }
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }

        [Authorize]
        [HttpPut("BundleActivation")]
        public async Task<ResponseDto> BundleActivation(int id)
        {
            try
            {
                var bundle = await _db.Bundles.FindAsync(id);
                if (bundle != null)
                {
                    if (bundle.IsActive == false)
                    {
                        bundle.IsActive = true;
                    }
                    else
                    {
                        bundle.IsActive = false;
                    }
                    await _db.SaveChangesAsync();
                    _response.Message = "Activation Successfully changed";
                }
                else
                {
                    throw new Exception("Bundle not found");
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }

        [HttpPut("ChangeProductPrice")]
        public async Task<ResponseDto> ChangeProductPrice(ProductDto product)
        {
            try
            {
                Product productFromDB = await _db.Products.FindAsync(product.ProductId);
                if (productFromDB != null)
                {
                    productFromDB.Price = product.Price;
                    productFromDB.DateUpdated = DateTime.Now;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Product doesn't exist");
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(ex.Message);
            }
            return _response;
        }


    }
}
