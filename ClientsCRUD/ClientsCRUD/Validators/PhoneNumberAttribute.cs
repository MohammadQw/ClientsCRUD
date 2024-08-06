using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace ClientsCRUD.Validators
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string phoneNumber)
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                try
                {
                    var number = phoneNumberUtil.Parse(phoneNumber, "JO");
                    return phoneNumberUtil.IsValidNumber(number);
                }
                catch (NumberParseException)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
