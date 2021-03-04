using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetAllDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetDetails(Expression<Func<Car, bool>> filter)
        {
            return GetAllDetailsBy(filter).SingleOrDefault();
        }

        public List<CarDetailDto> GetAllDetailsBy(Expression<Func<Car, bool>> filter)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars.Where(filter)
                         join b in context.Brands
                         on c.BrandId equals b.Id
                         join cl in context.Colors
                         on c.ColorId equals cl.Id
                         select new CarDetailDto
                         {
                             CarId = c.CarId,
                             CarName = c.CarName,
                             BrandName = b.Name,
                             ColorName = cl.Name,
                             ModelYear = c.ModelYear,
                             DailyPrice = c.DailyPrice,
                             Description = c.Description
                         };
                
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetRentableDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                //var query =
                //    from c in context.Cars
                //    where !(from r in context.Rentals
                //            select r.CarId)
                //           .Contains(c.CarId)
                //           join b in context.Brands
                //           on c.BrandId equals b.Id
                //    select c;
                //var cars = !from r in context.Rentals
                //from c in context.Cars.Where(x => r.CarId.Equals(x.CarId)).ToList();
                //var rentals = (from r in context.Rentals.Contains select r.CarId);
                var result = 
                    from c in context.Cars
                    where !(from r in context.Rentals
                           select r.CarId)
                    .Contains(c.CarId)
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };
                return result.ToList();
            }

        }

        public List<CarDetailDto> GetRentedDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                //var query =
                //    from c in context.Cars
                //    where !(from r in context.Rentals
                //            select r.CarId)
                //           .Contains(c.CarId)
                //           join b in context.Brands
                //           on c.BrandId equals b.Id
                //    select c;
                //var cars = !from r in context.Rentals
                //from c in context.Cars.Where(x => r.CarId.Equals(x.CarId)).ToList();
                //var rentals = (from r in context.Rentals.Contains select r.CarId);
                var result =
                    from c in context.Cars
                    where (from r in context.Rentals
                            select r.CarId)
                    .Contains(c.CarId)
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    select new CarDetailDto
                    {
                        CarId = c.CarId,
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = cl.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description
                    };
                return result.ToList();
            }
        }
    }
}
