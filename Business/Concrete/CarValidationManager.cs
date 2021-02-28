using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarValidationManager : ICarValidationService
    {
        public IResult Validate(Car car)
        {
            bool result = (NameValidate(car).Success && DailyPriceValidate(car).Success);
            string message = NameValidate(car).Message + "\n" + DailyPriceValidate(car).Message;
            if (result) 
            {
                return new SuccessResult(message);
            }
            else
            {
                return new ErrorResult(message);
            }
        }
        public IResult UpdateValidate(Car car)
        {
            bool result = (NameValidate(car).Success && DailyPriceValidate(car).Success);
            string message = NameValidate(car).Message + "\n" + DailyPriceValidate(car).Message;
            if (result)
            {
                return new SuccessResult(message);
            }
            else
            {
                return new ErrorResult(message);
            }
        }

        private IResult DailyPriceValidate(Car car)
        {
            if (car.DailyPrice > 0)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(Messages.DailyPriceGreater);
            }
        }

        private IResult NameValidate(Car car)
        {
            if (car.CarName.Length >= 2)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(Messages.CarNameLeast);
            }
        }


    }
}
