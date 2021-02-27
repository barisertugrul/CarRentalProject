using Business.Abstract;
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

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public void Add(Car car)
        {
            if (_carValidationService.Validate(car))
            {
                _carDal.Add(car);
            }
            else
            {
                Console.WriteLine("Adding could not be performed because some fields do not conform to the valid data rules.");
            }
        }

        public void Update(Car car)
        {
            if (_carValidationService.Validate(car))
            {
                _carDal.Update(car);
            }
            else
            {
                Console.WriteLine("Adding could not be performed because some fields do not conform to the valid data rules.");
            }
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public Car GetById(int id)
        {
            return _carDal.Get(c => c.CarId == id);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetails();
        }

        public List<CarDetailDto> GetCarDetailsBy(Expression<Func<Car, bool>> filter = null)
        {
            return _carDal.GetCarDetailsBy(filter);
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            return _carDal.GetCarDetailsBy(c => c.ColorId == colorId);
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            return _carDal.GetCarDetailsBy(c => c.BrandId == brandId);
        }

        public int CarsCount()
        {
            return _carDal.GetAll().Count;
        }
    }
}
