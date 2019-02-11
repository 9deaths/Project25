using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication25.Models;
using WebApplication25.Controllers;

namespace WebApplication25.DataBaseContexts
{
    public class DataBaseContext : DbContext
    {
        //используем подключение "DB"
        public DataBaseContext() : base("name=DB")
        {

        }
                
        public DbSet<AddressBookEntry> AddressBookContext { get; set; }

    } 
}