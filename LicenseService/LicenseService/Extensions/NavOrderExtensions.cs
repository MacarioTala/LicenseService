using System.Collections.Generic;
using System.Linq;
using LicenseService.Entities;

namespace LicenseService.Extensions
{
    public static class NavOrderExtensions
    {
        
        public static LicenseMessage IsOrderNumberNullOrEmpty(this NavOrder order)
        {
            return string.IsNullOrEmpty(order.OrderNumber)
                ? new LicenseMessage
                {
                    Code = ErrorGuids.OrderNumberMissingGuid,
                    Message = "OrderNumber is missing",
                    Severity = (int) MessageStateEnum.Error
                }
                : null;
        }

        public static LicenseMessage IsOrderNumberGreaterThan20(this NavOrder order)
        {
            return order.OrderNumber.Length > 20
                ? new LicenseMessage
                {
                    Code = ErrorGuids.OrderNumberIsTooLongGuid,
                    Message = "OrderNumber " + order.OrderNumber + " is too long",
                    Severity = (int) MessageStateEnum.Error
                }
                : null;
        }

        public static LicenseMessage IsActionValid(this NavOrder order)
        {
            if (order.Action != 1 && order.Action != 0)
            {
                return new LicenseMessage
                {
                    Code = ErrorGuids.InvalidActionGuid,
                    Message = "Action is invalid",
                    Severity = (int) MessageStateEnum.Error
                };
            }
            return null;
        }

        public static LicenseMessage IsProductOrdersEmpty(this NavOrder order)
        {
            if(order.ProductOrders==null||!order.ProductOrders.Any())
            {
                return new LicenseMessage
                {
                    Code = ErrorGuids.NoProductsInOrderGuid,
                    Message = "No product orders in order",
                    Severity = (int)MessageStateEnum.Error
                };
            }
            return null;
        }

        public static List<LicenseMessage> AreThereEmptyLicenseKeys(this NavOrder order)
        {
            return (from productOrder in order.ProductOrders
                where string.IsNullOrEmpty(productOrder.LicenseKey)
                select new LicenseMessage
                {
                    Code = ErrorGuids.LicenseKeyIsEmptyGuid,
                    Message = "Empty LicenseKey for product "+productOrder.ProductName+"in order",
                    Severity = (int) MessageStateEnum.Error
                }).ToList();
        }

        public static List<LicenseMessage> DoesProductListContainLicenseKey(this NavOrder order, List<string> licenseKeys)
        {
            return (from productOrder in order.ProductOrders
                where licenseKeys.Contains(productOrder.LicenseKey)
                select new LicenseMessage
                {
                    Code = ErrorGuids.LicenseKeyAlreadyAssigned
                    , Message = "LicenseKey " + productOrder.LicenseKey + " is already assigned"
                    , Severity = (int) MessageStateEnum.Error
                }).ToList();
        }

        public static List<LicenseMessage> DoesLicenseKeyExistForUpdate(this NavOrder order, List<string> licenseKeys)
        {
            return (from productOrder in order.ProductOrders
                    where !licenseKeys.Contains(productOrder.LicenseKey)
                    select new LicenseMessage
                    {
                        Code = ErrorGuids.LicenseKeyDoesntExist
                        ,
                        Message = "LicenseKey " + productOrder.LicenseKey + " does not exist"
                        ,
                        Severity = (int)MessageStateEnum.Error
                    }).ToList();
        }
    }
}
