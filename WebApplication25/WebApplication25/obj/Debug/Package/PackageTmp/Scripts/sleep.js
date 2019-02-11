
//когда страница загружена
$(document).ready(
   
    function () {
       
        $.ajaxSetup({ cache: false });

        //  ссылке прикрепляется обработчик нажатия
        $(".showmeDialog").on("click", function (e) {
            //отменить все обработчики события кроме следующих далее
            e.preventDefault();

            //выводит элемент в виде диалогового окна 
            $("<div id='urog'></div>").addClass("dialog").appendTo("body").dialog({
                //
                title: $(this).attr("data-dialog-title"), close: function () {
                    $("div[tabindex = '-1']").remove()
                    //модальность
                }, modal: false
                //загрузка данных из БД
            }).load("/Home/Create");
        });

        //в случае недоступности БД сообщаем
        var infoc = $("i[name='info']").text();
        if (infoc.includes("Данные не загружены (отсутствует подключение к БД)..."))
            alert(infoc);
    });


function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


async function responseAggregate() { 

    $("i[name='infocreate']").text("Идет проверка данных...");

    //сон для видимости обработки запроса
    await sleep(2000);

    //получение значений информационных элементов
    var info = $("i[name='info']").text();
    var infocreate = $("i[name='infocreate']").text(); 

    //ответ от контроллера считываем с элемента Session и выводим информационные окна

    if (info.includes("Error! (Адрес уже существует)")) {
        alert("Error! (Адрес уже существует)");
        $("i[name='infocreate']").html("Введите  <b>другие</b> данные местоположения (Адрес уже существует)");
        
        return false;
    }
    else {
        
        if (infocreate.includes("Идет проверка данных...")) {
           
            $("i[name='infocreate']").html("Адрес успешно занесен в базу");
            $("div[tabindex = '-1']").remove()
            alert("Адрес успешно занесен в базу");
            
            return true;
        }
        //в случае любых других ошибок
        else {
            alert(info);
            return false;
        }
    }
        
    return false;
}

