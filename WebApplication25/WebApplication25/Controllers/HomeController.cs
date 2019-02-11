using System;
using System.Web.Mvc;
using WebApplication25.Models;
using System.Data.Entity;
using WebApplication25.DataBaseContexts;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;



namespace WebApplication25.Controllers
{
    public class HomeController : Controller
    {


        CreateState stateCreate = new CreateState();

      //  DataBaseContext db = new DataBaseContext();



        public ActionResult Index()
        {
            
            DataBaseContext db = stateCreate.dbCreate;
            Session["NewInfo"] = stateCreate.errCreate;
            // перед загрузкой данных в таблицу проверяем существование подключаемой БД 
            if (!db.Database.Exists())
                {                    
                    //создаем список всех ошибок  
                    ModelState.AddModelError("Book", "БД не существует");

                    //сохраняем ошибку для вывода в отдельном представлении 
                    Session["Error"] = "Данные не загружены (отсутствует подключение к БД)...";
                    Session["Info"] = Session["Error"];
                    Session["Alert"] = true;

                    return View();
                }

            //инициализация при первом запуске
            if (Session["Alert"] == null)
            {                
                    Session["Error"] = "Ошибок нет";
                    Session["Alert"] = false;
                    Session["Info"] = "Данные успешно загружены";                
            }

            //проверка после попытки добавления записи 
            else if ((bool)Session["Alert"])
            {
                Session["Info"] = Session["Error"];
            }
                //вывод прдеставления с отсортированным списком
                return View(db.AddressBookContext.OrderBy(c => c.Country).ThenBy(b => b.City));        
            
        }
  
            public ActionResult Test(int? multi)
        {
            int count=9;
            if (multi !=null)
            count=+(int)multi;
            DateTime dates = DateTime.Now; 
            ViewBag.data = "test"+DateTime.Now.ToString();

            return View(count);

        }



        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]        
        public ActionResult Create(AddressBookEntry book)
        {           
          
          //  Thread.Sleep(2000);
           
            book.DateAdded = DateTime.Now; 
            
            //перехватываем ошибки перед добавлением новой записи в БД
            if (ModelState.IsValid)
            {
                try
                {
                    DataBaseContext db = stateCreate.dbCreate;
                    if (!(db.AddressBookContext.Any(a => a.City == book.City) &&
                        db.AddressBookContext.Any(b => b.Street == book.Street) &&
                         db.AddressBookContext.Any(c => c.HomeNumber == book.HomeNumber)))
                    {                          
                        book.DateAdded = DateTime.Now;
                        //добавляем в конетекст новую созданную сущность-адрес
                        db.AddressBookContext.Add(book);
                        //сохраняем изменения в конексте в БД
                        db.SaveChanges();

                        Session["Alert"] = false;                       
                        Session["Info"] = "Адрес успешно добавлен.";
                        
                        return RedirectToAction("Index");
                       
                    }
                    else
                    {
                        Session["Alert"] = true;                      
                        Session["Error"] = "Error! (Адрес уже существует)";                       

                        return RedirectToAction("Index");                      
                    }
                }

                //ошибки при записи в БД
                catch (DbEntityValidationException e)
                {
                    Session["Error"] = "Сохранение было прервано, поскольку не удалось проверить достоверность значений свойств объекта. " + e.Message;
                    Session["Alert"] = true;                    
                    return RedirectToAction("Index");

                }

                catch (InvalidOperationException e)
                {
                    Session["Error"] = "Произошла некоторая ошибка при попытке обработать сущности в контексте"+ e.Message; 
                    Session["Alert"] = true;                  
                    return RedirectToAction("Index");
                }

                catch (DbUpdateException e)
                {
                    Session["Error"] = "Произошла ошибка при отправке обновлений в базу данных."+e.Message;
                    Session["Alert"] = true;                    
                    return RedirectToAction("Index");
                }

                catch (Exception e)
                {
                    
                    Session["Error"] = "Ошибка при обновлении данных"+ e.Message;                   
                    Session["Alert"] = true;
                    return RedirectToAction("Index");                    
                }

            }
            else
            {
                
                Session["Error"] = "Ошибка при сохранения данных";
                Session["Alert"] = true;               

                return RedirectToAction("Index");
            }
           
        }
       
    }
}
