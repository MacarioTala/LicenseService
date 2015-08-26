using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LicenseService.Constants;
using LicenseService.Entities;
using LicenseService.Enums;

[assembly: InternalsVisibleTo("LicenseServiceUnitTests")]
namespace LicenseService.Extensions
{
    ///TODO: Todo Date:20150824
    /// If the todo date has passed, we've lost
    /// All of these need to throw exceptions instead of returning LicenseMessages. We're currently not storing these anyway.
    
    public static class NavOrderExtensions
    {

        internal static List<LicenseMessage> DoProductsInOrderExist()
        {
            throw new NotImplementedException();
        }

        internal static List<LicenseMessage> DoProductsInOrderHaveValidExpirationDates()
        {
            throw new NotImplementedException();
        }

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

        internal static List<LicenseMessage> DoesLicenseKeyExistForUpdate(this NavOrder order, List<License> existingLicenses)
        {
            return (from productOrder in order.ProductOrders
                    where !existingLicenses.GetLicenseKeysFromLicenseList().Contains(productOrder.LicenseKey)
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

        internal static List<LicenseMessage> ValidateOrderForNewLicense(this NavOrder order, List<string> existingLicenseList )
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
            List<License> existingLicenses )
        {
            return (from productOrder in order.ProductOrders
                from existingLicense in existingLicenses
                where productOrder.LicenseKey == existingLicense.LicenseKey && productOrder.NumberOfUsers < existingLicense.Users.Count
                select new LicenseMessage
                {
                    Code = ErrorGuids.NumberOfAssignedUsersExceedsLicensedUsersInOrderGuid, Message = "Current number of assigned users for license: " + existingLicense.LicenseKey + "exceeds number of users(" + existingLicense.NumberOfUsers + ") in order.", Severity = (int) MessageStateEnum.Error
                }).ToList();
        }

        internal static List<LicenseMessage> DoOrderNumbersMatchExistingLicenseOrderNumbers(this NavOrder order,
            List<License> licenses)
        {
            return (from productOrder in order.ProductOrders
                from license in licenses
                where productOrder.LicenseKey == license.LicenseKey && order.OrderNumber != license.OrderNumber
                select new LicenseMessage
                {
                    Code = ErrorGuids.OrderNumberDoesntMatchGuid, Message = "License Key:" + license.LicenseKey + " does not match current order number(" + order.OrderNumber + ")", Severity = (int) MessageStateEnum.Error
                }).ToList();
        }

        internal static List<LicenseMessage> ValidateUpdateOrder(this NavOrder order, List<License> existingLicenses)
        {
            var returnMessageList = new List<LicenseMessage>();
            
            returnMessageList.AddRange(order.DoesLicenseKeyExistForUpdate(existingLicenses));
            returnMessageList.AddRange(order.DoExistingUsersExceedUsersInOrder(existingLicenses));
            returnMessageList.AddRange(order.DoOrderNumbersMatchExistingLicenseOrderNumbers(existingLicenses));
            
            return returnMessageList;
        }

        public static List<LicenseMessage> ValidateThis(this NavOrder order, List<License> licensesInOrder)
        {
            var returnMessages = new List<LicenseMessage>();
            if (order.IsActionValid() == null)
            {
                returnMessages.Add(order.IsActionValid());
                return returnMessages;
            }
            
            returnMessages.Add(order.IsProductOrdersEmpty());
            returnMessages.Add(order.IsOrderNumberGreaterThan20());
            returnMessages.AddRange(order.DoesNumberOfUsersExceed5000());
            returnMessages.Add(order.IsOrderNumberNullOrEmpty());
            returnMessages.Add(order.IsProductOrdersEmpty());
            returnMessages.AddRange(order.AreThereEmptyLicenseKeys());    

            if (order.Action == (int) ActionEnum.Create)
            {
                order.ValidateOrderForNewLicense(licensesInOrder.GetLicenseKeysFromLicenseList());
            }
            else
            {
                order.ValidateUpdateOrder(licensesInOrder);
            }

            return returnMessages;
        }
    }
}
