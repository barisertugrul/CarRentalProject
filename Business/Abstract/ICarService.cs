using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car GetById(int id);
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);
        List<CarDetailDto> GetCarDetails();
        List<CarDetailDto> GetCarDetailsByColorId(int colorId);
        List<CarDetailDto> GetCarDetailsByBrandId(int brandId);
        int CarsCount();
    }
}
