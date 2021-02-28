using Business.Concrete;
using Business.Constants;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI.Concrete
{
    public class MainConsoleManager
    {
        /****************************************************************************
         * 
         * Console işlemleri merkezi
         * 
         ***************************************************************************/
        /* 
         * Performans açısından manager sınıflarını burada atamak mı
         * Yoksa yeri geldikçe, fonksiyonlar içinde tekrar tekrar newlemek mi 
         * daha mantıklı bilemiyorum. Şimdilik newlemeyi burada yapıyorum
         */

        CarManager _carManager = new CarManager(new EfCarDal(), new CarValidationManager());
        BrandManager _brandManager = new BrandManager(new EfBrandDal());
        ColorManager _colorManager = new ColorManager(new EfColorDal());

        public void MainMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Car Manager", "2-Brand Manager", "3-Color Manager", "4-Settings", "5-EXIT" };
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.MainMenuTitle, menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectNumberOfMenuItem);
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    CarMenu();
                    MainMenu();
                    break;
                case 2:
                    BrandMenu();
                    MainMenu();
                    break;
                case 3:
                    ColorMenu();
                    MainMenu();
                    break;
                case 4:
                    //ListMenu();
                    //MainMenu();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(Messages.WrongChoice);
                    MainMenu();
                    break;
            }
        }

        private void ColorMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Add New Color", "2-Update a Color", "3-Delete a Color", "4-View Colors List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ColorMenuTitle, menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectNumberOfMenuItem);
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddColor();
                    ColorMenu();
                    break;
                case 2:
                    UpdateColor();
                    ColorMenu();
                    break;
                case 3:
                    DeleteColor();
                    ColorMenu();
                    break;
                case 4:
                    ListColors();
                    ColorMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine(Messages.WrongChoice);
                    ColorMenu();
                    break;
            }
        }

        private void BrandMenu()
        {
            string consoleVal;

            string[] menuItems = new string[] { "1-Add New Brand", "2-Update a Brand", "3-Delete a Brand", "4-View Brands List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.BrandMenuTitle, menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectNumberOfMenuItem);
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddBrand();
                    BrandMenu();
                    break;
                case 2:
                    UpdateBrand();
                    BrandMenu();
                    break;
                case 3:
                    DeleteBrand();
                    BrandMenu();
                    break;
                case 4:
                    ListBrands();
                    BrandMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine(Messages.WrongChoice);
                    BrandMenu();
                    break;
            }
        }

        private void CarMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Add New Car", "2-Update a Car", "3-Delete a Car", "4-View Cars List",
                "5-Cars by Brand", "6-Cars by Color", "7-RETURN MAIN MENU" };

            ConsoleTexts.WriteConsoleMenuInFrame(Messages.CarMenuTitle, menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectNumberOfMenuItem);
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddCar();
                    CarMenu();
                    break;
                case 2:
                    UpdateCar();
                    CarMenu();
                    break;
                case 3:
                    DeleteCar();
                    CarMenu();
                    break;
                case 4:
                    ListCars();
                    CarMenu();
                    break;
                case 5:
                    CarsByBrand();
                    CarMenu();
                    break;
                case 6:
                    CarsByColor();
                    CarMenu();
                    break;
                case 7:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine(Messages.WrongChoice);
                    CarMenu();
                    break;
            }
        }

        private void AddCar()
        {
            Car car = new Car();
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderCarAddNew);
            ConsoleTexts.FrameHeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeCarName);
            car.CarName = consoleVal;

            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrandSelect, StrBrandList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectBrandId);
            car.BrandId = Convert.ToInt32(consoleVal);

            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColorSelect, StrColorList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectColorId);
            car.ColorId = Convert.ToInt32(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeModelYear);
            car.ModelYear = Convert.ToInt16(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeDailyPrice);
            car.DailyPrice = Convert.ToDecimal(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeDescription);
            car.Description = consoleVal;

            _carManager.Add(car);
        }

        private void UpdateCar()
        {
            Car car;
            string consoleVal;

            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderCarUpdate);
            ConsoleTexts.FrameHeaderFooterLine();
            ListCars();
            if (_carManager.CarsCount().Data > 0)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectCarIdToUpdate);
                car = _carManager.GetById(Convert.ToInt32(consoleVal)).Data;

                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeCarName + Messages.LeaveBlank);
                car.CarName = consoleVal;
                if (consoleVal != "")
                {
                    car.CarName = consoleVal;
                }

                ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrandSelect, StrBrandList());
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectBrandId + Messages.LeaveBlank);
                if (consoleVal != "")
                {
                    car.BrandId = Convert.ToInt32(consoleVal);
                }

                ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColorSelect, StrColorList());
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectColorId + Messages.LeaveBlank);
                if (consoleVal != "")
                {
                    car.ColorId = Convert.ToInt32(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeModelYear + Messages.LeaveBlank);
                if (consoleVal != "")
                {
                    car.ModelYear = Convert.ToInt16(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeDailyPrice + Messages.LeaveBlank);
                if (consoleVal != "")
                {
                    car.DailyPrice = Convert.ToDecimal(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeDescription + Messages.LeaveBlank);
                if (consoleVal != "")
                {
                    car.Description = consoleVal;
                }

                _carManager.Update(car);
            }
            else
            {
                CarMenu();
            }
        }

        private void DeleteCar()
        {
            Car car;
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderCarDelete);
            ConsoleTexts.FrameHeaderFooterLine();
            ListCars();
            if (_carManager.CarsCount().Data > 0)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectCarIdToDelete);
                if ( consoleVal != "")
                {
                    car = _carManager.GetById(Convert.ToInt32(consoleVal)).Data;
                    if (ConsoleTexts.ConfirmAction(Messages.DeleteItemAttention)) _carManager.Delete(car);
                }
            }
        }

        private  void ListCars()
        {
            List<CarDetailDto> cars = _carManager.GetCarDetails().Data;
            string[] headers = {"Car ID", "Car Name", "Brand name", "Color Name", "Model Year", "Daily Price", "Description" };
            ConsoleTexts.WriteDataList<CarDetailDto>(Messages.ListHeaderCar, cars, headers);
        }

        private void CarsByColor()
        {
            string consoleVal;
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColorSelect, StrColorList());
            //ConsoleTexts.WriteDataList("COLORS", _colorManager.GetAll());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectColorId);
            int colorId = Convert.ToInt32(consoleVal);

            string color = _colorManager.GetById(colorId).Data.Name;
            List<CarDetailDto> cars = _carManager.GetCarDetailsByColorId(colorId).Data;
            ConsoleTexts.WriteDataList(color + Messages.ListHeaderColoredCar, cars);
        }

        private void CarsByBrand()
        {
            string consoleVal;
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrandSelect, StrBrandList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectBrandId);
            int brandId = Convert.ToInt32(consoleVal);

            string brand = _brandManager.GetById(brandId).Data.Name;
            List<CarDetailDto> cars = _carManager.GetCarDetailsByBrandId(brandId).Data;
            ConsoleTexts.WriteDataList(brand + Messages.ListHeaderBrandedCar, cars);
        }

        private void AddColor()
        {
            Color color = new Color();
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderColorAddNew);
            ConsoleTexts.FrameHeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeColorName);
            color.Name = consoleVal;

            _colorManager.Add(color);
        }

        private void UpdateColor()
        {
            Color color;
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderColorUpdate);
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColorSelect, StrColorList());
            if (StrColorList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectColorIdToUpdate);
                if (consoleVal != "")
                { 
                    color = _colorManager.GetById(Convert.ToInt32(consoleVal)).Data;

                    consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeColorName + Messages.LeaveBlank );
                    if (consoleVal != "")
                    {
                        color.Name = consoleVal;
                    }
                    _colorManager.Update(color);
                }
            }
        }

        private void DeleteColor()
        {
            Color color;
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderColorDelete);
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColorSelect, StrColorList());
            if (StrColorList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectColorIdToDelete);
                if (consoleVal != "")
                {
                    color = _colorManager.GetById(Convert.ToInt32(consoleVal)).Data;
                    if (ConsoleTexts.ConfirmAction(Messages.DeleteItemAttention)) _colorManager.Delete(color);
                }
            }
        }

        private void ListColors()
        {
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderColor, StrColorList());
        }

        private string[] StrColorList()
        {
            string[] colorList = new string[_colorManager.GetAll().Data.Count];
            int i = 0;
            foreach (Color color in _colorManager.GetAll().Data)
            {
                colorList[i] = Messages.IDTag + color.Id + " - " + color.Name;
                i++;
            }
            return colorList;
        }

        private void AddBrand()
        {
            Brand brand = new Brand();
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderBrandAddNew);
            ConsoleTexts.FrameHeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeBrandName);
            brand.Name = consoleVal;
            _brandManager.Add(brand);
        }

        private void UpdateBrand()
        {
            Brand brand;
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderBrandUpdate);
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrandSelect, StrBrandList());
            if (StrBrandList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectBrandIdToUpdate);
                if ( consoleVal != "" )
                {
                    brand = _brandManager.GetById(Convert.ToInt32(consoleVal)).Data;
                    consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.TypeBrandName + Messages.LeaveBlank);
                    if (consoleVal != "")
                    {
                        brand.Name = consoleVal;
                    }
                    _brandManager.Update(brand);
                }
            }
        }

        private void DeleteBrand()
        {
            Brand brand;
            string consoleVal;
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.Header(Messages.FormHeaderBrandDelete);
            ConsoleTexts.FrameHeaderFooterLine();
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrandSelect, StrBrandList());
            if (StrBrandList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine(Messages.SelectBrandIdToDelete);
                if ( consoleVal != "")
                {
                    brand = _brandManager.GetById(Convert.ToInt32(consoleVal)).Data;
                    if (ConsoleTexts.ConfirmAction(Messages.DeleteItemAttention)) _brandManager.Delete(brand);
                }
            }
        }

        private void ListBrands()
        {
            ConsoleTexts.WriteConsoleMenuInFrame(Messages.ListHeaderBrand, StrBrandList());
        }

        private string[] StrBrandList()
        {
            string[] brandList = new string[_brandManager.GetAll().Data.Count];
            int i = 0;
            foreach (Brand brand in _brandManager.GetAll().Data)
            {
                brandList[i] = Messages.IDTag + brand.Id + " - " + brand.Name;
                i++;
            }
            return brandList;
        }
    }
}
