using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LicenseService.Entities;

[assembly: InternalsVisibleTo("LicenseServiceUnitTests")]
namespace LicenseService.Extensions
{
    public static class NavOrderExtensions
    {
        
        internal static LicenseMessage IsOrderNumberNullOrEmpty(this NavOrder order)
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

        internal static LicenseMessage IsOrderNumberGreaterThan20(this NavOrder order)
        {
            if(order.OrderNumber==null)
            {
                return null;
            }
            return order.OrderNumber.Length > 20
                ? new LicenseMessage
                {
                    Code = ErrorGuids.OrderNumberIsTooLongGuid,
                    Message = "OrderNumber " + order.OrderNumber + " is too long",
                    Severity = (int) MessageStateEnum.Error
                }
                : null;
        }

        internal static LicenseMessage IsActionValid(this NavOrder order)
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

        internal static LicenseMessage IsProductOrdersEmpty(this NavOrder order)
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

        internal static List<LicenseMessage> AreThereEmptyLicenseKeys(this NavOrder order)
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

        internal static List<LicenseMessage> DoesProductListContainLicenseKey(this NavOrder order, List<string> licenseKeys)
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

        internal static List<LicenseMessage> DoesLicenseKeyExistForUpdate(this NavOrder order, List<string> licenseKeys)
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

        internal static List<LicenseMessage> DoesNumberOfUsersExceed5000(this NavOrder order)
        {
            const int maxNumberOfUsers = 5000;
            return (from productOrder in order.ProductOrders
                where productOrder.NumberOfUsers > maxNumberOfUsers
                select new LicenseMessage
                {
                    Code = ErrorGuids.NumberOfUsersIsInvalid,
                    Message = "Number of users is invalid",
                    Severity = (int) MessageStateEnum.Error
                }).ToList();
        }

        internal static List<LicenseMessage> CheckParameters(this NavOrder order)
        {
            var returnMessageList = new List<LicenseMessage>();

            returnMessageList.AddIfNotNull(order.IsOrderNumberNullOrEmpty());
            returnMessageList.AddIfNotNull(order.IsOrderNumberGreaterThan20());
            returnMessageList.AddIfNotNull(order.IsActionValid());
            returnMessageList.AddIfNotNull(order.IsProductOrdersEmpty());

            return returnMessageList;
        }

        internal static List<LicenseMessage> ValidateOrdersForNewLicense(this NavOrder order, List<string> existingLicenseList )
        {
            var returnMessageList = new List<LicenseMessage>();

            foreach (var licenseMessage in order.AreThereEmptyLicenseKeys() )
            {
                returnMessageList.AddIfNotNull(licenseMessage);    
            }

            foreach (var licenseMessage in order.DoesProductListContainLicenseKey(existingLicenseList))
            {
                returnMessageList.AddIfNotNull(licenseMessage);
            }

            return returnMessageList;
        }


        internal static List<LicenseMessage> DoExistingUsersExceedUsersInOrder(this NavOrder order,
            Dictionary<string,int> usersPerLicenceKey)
        {
            return (from productOrder in order.ProductOrders
                from licenseKeyItem in usersPerLicenceKey
                where licenseKeyItem.Value > productOrder.NumberOfUsers
                select new LicenseMessage
                {
                    Code = ErrorGuids.NumberOfAssignedUsersExceedsLicensedUsersInOrderGuid, 
                    Message = "Current number of assigned users for license: " + licenseKeyItem.Key + "exceeds number of users(" + licenseKeyItem.Value + ") in order."
                    , Severity = (int) MessageStateEnum.Error
                }).ToList();
        }
    }
}
