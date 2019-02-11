using System;

using WebApplication25.DataBaseContexts;

namespace WebApplication25.Models
{
    public class CreateState
    {

        public DataBaseContext dbCreate
        {
            get; set;
        }

        public string errCreate
        {
            get; set;
        }

        public  CreateState()
        {  
         try{

                dbCreate = new DataBaseContext();

                errCreate = "Успешно";
                return ;
             }

         catch (Exception e)
            {
                errCreate = e.Message;
                return ;

            }



        }   

    }

}


