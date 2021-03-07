using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarValidationService _carValidationService;

        public CarManager(ICarDal carDal, ICarValidationService carValidationService)
        {
            _carDal = carDal;
            _carValidationService = carValidationService;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IResult Add(Car car)
        {
            IResult result = _carValidationService.Validate(car);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.NewCarAdded);
        }

        public IResult Update(Car car)
        {
            IResult result = _carValidationService.UpdateValidate(car);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetails());
        }

        public IDataResult<CarDetailDto> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetDetails(c=> c.CarId == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetailsBy(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetailsBy(c => c.BrandId == brandId));
        }

        public IDataResult<int> Count()
        {
            return new SuccessDataResult<int>(_carDal.GetAll().Count);
        }

        public IDataResult<List<CarDetailDto>> GetRentableCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetRentableDetails());
        }

        public IDataResult<List<CarDetailDto>> GetRentedCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetRentedDetails());
        }
    }
}
