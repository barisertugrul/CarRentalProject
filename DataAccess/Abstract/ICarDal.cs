using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetAllDetails();
        List<CarDetailDto> GetAllDetailsBy(Expression<Func<Car, bool>> filter);
        CarDetailDto GetDetails(Expression<Func<Car, bool>> filter);
        List<CarDetailDto> GetRentableDetails();
        List<CarDetailDto> GetRentedDetails();
    }
}
