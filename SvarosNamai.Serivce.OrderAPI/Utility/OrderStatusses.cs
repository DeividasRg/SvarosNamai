using System;

namespace SvarosNamai.Serivce.OrderAPI.Utility
{
    public class OrderStatusses
    {
        public const int Status_Pending = 0;
        public const int Status_Approved = 1;
        public const int Status_Completed = 2;
        public const int Status_Cancelled = -1;
        public const int Status_Addition = 5;


        public static int GetStatusConstant(int status)
        {
            switch (status)
            {
                case Status_Pending:
                    return Status_Pending;
                case Status_Approved:
                    return Status_Approved;
                case Status_Completed:
                    return Status_Completed;
                case Status_Cancelled:
                    return Status_Cancelled;
                case Status_Addition:
                    return Status_Addition;
                default:
                    return 99;
            }
        }
    }
}

