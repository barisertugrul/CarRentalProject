﻿using Core.DataAccess.EntityFramework;
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
        public List<CarDetailDto> GetCarDetails()
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

        public List<CarDetailDto> GetCarDetailsBy(Expression<Func<Car, bool>> filter = null)
        {
            IQueryable<CarDetailDto> result;
            using (RentACarContext context = new RentACarContext())
            {
                if (filter == null)
                {
                    result = from c in context.Cars
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
                }
                else
                {
                    result = from c in context.Cars.Where(filter)
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
                }
                return result.ToList();
            }
        }
    }
}