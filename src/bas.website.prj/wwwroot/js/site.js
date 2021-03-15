

$('.currency-button').on('click', function(e){
    $('.currency-button').removeClass('currency-active');
    $('.currency-button').attr('data-currency-active', 'false');
    $(this).addClass('currency-active');
    $(this).attr('data-currency-active', 'true')
});


$('.indiv-percent').on('click', function(e){


    var sel = parseFloat($('.calc-select').val());

    var percent = $.cookie('UserPercent');

    console.log(sel)
    console.log(percent)

    $(this).parent('.calc-to-block').children('.calc-input').val((sel + parseFloat(percent)).toFixed(1));

    $(this).parent('.calc-to-block').children('.calc-input').addClass('calc-input-active');

});

$('.calc-input').on('click', function(e){
    $(this).removeClass('calc-input-active');
});


$('.deadline-button').on('click', function(e){
    $('.deadline-button').removeClass('currency-active');
    $('.deadline-button').attr('data-deadline-active', 'false');
    $(this).addClass('currency-active');
    $(this).attr('data-deadline-active', 'true')
});


$('.new-deadline-button').on('click', function(e){

    if ($(this).data('new-deadline') == 'yes') {
        $(this).parent('.calc-to-block').children('.calc-input').removeClass('calc-input-hide');
    }
    else{
        $(this).parent('.calc-to-block').children('.calc-input').addClass('calc-input-hide');
    }
    $('.new-deadline-button').removeClass('currency-active');
    $('.new-deadline-button').attr('data-new-deadline-active', 'false');
    $(this).addClass('currency-active');
    $(this).attr('data-new-deadline-active', 'true');

});

function GetFdate() {

    var ft = $('#fdate');
    if (ft.children('.currency-active').attr('data-new-deadline-active') == 'true'){
        return ft.children('.calc-input').val();
    }
    return false;
}

$('#get-value-block').on('click', function(e){

    // Сумма кредита
    var sum = $('#sum').val();
    // Валюта кредита
    var cur = $('#cur').children('.currency-active').attr('data-currency');
    // Информация о целях кредита(вычетаем)
    var pur = $('#pur').val();
    // Процентная ставка
    var rate = $('#rate').val();
    // Срок займа в месяцах
    
    var ddly = Number($('#ddly').val());
    console.log(ddly)
    var ddlm = Number($('#ddlm').val()) + (ddly * 12);
    console.log(ddlm)
    // Дата выдачи
    var sdate = $('#sdate').val();
    // Порядок погашения
    var tpay = $('#tpay').val();
    // Периодичность погашения
    var perpay = $('#perpay').val();
    // Сроки досрочного погашения
    var ft = $('#fdate');
    // Подготовка URL
    var url = window.location.href  + `/out/?sum=${sum}&cur=${cur}&pur=${-pur}&rate=${rate}&ddlm=${ddlm}&sdate=${sdate}&tpay=${tpay}&perpay=${perpay}`;
    // Подготовка U
    if (ft.children('.currency-active').attr('data-new-deadline-active') == 'true'){
        var fdate = ft.children('.calc-input').val();
        url += `&fdate=${fdate}`
    }

    window.location.href = url;

});


function checkCur(e){
    let cur = $('#cur').children('.currency-active').attr('data-currency')
    let sum = $('#sum').val();
    console.log(cur);

    if (cur == 'rub' & sum < 1000) {
        alert("Минимум 1000 рублей");
        $('#sum').val(1000);
    }
    else if (cur == 'eur' & sum < 100) {
        alert("Минимум 100 евро");
        $('#sum').val(100);
    }
    else if (cur == 'dol' & sum < 100) {
        alert("Минимум 100 долларов");
        $('#sum').val(100);
    }

}

$('.currency-button').on('click', function(e){
    checkCur();
});
$('#sum').change(function(e){
    checkCur();
});

$('#rate').change(function(e){
    if ($('#rate').val() < 0.1) {
        alert("Минимум 0.1%");
        $('#rate').val(0.1);
    }
});


// Название блоков


$('#ddly').on('input', function(e){

    if ($('#ddly').val() < 0){
        alert("Меньше нуля!");
        $('#ddly').val(0);
    } else {
        let z = $('#ddly').val();
        if (z == 0) $('.ddly').text("Месяц(a)");
        else if( z >= 11 & z  < 20) $('.ddly').text("Лет");
        else if (z % 10 == 1) $('.ddly').text("Год");
        else if( z % 10 > 1 & z % 10 <= 4) $('.ddly').text("Года");
        else $('.ddly').text("Лет");
    }
});

$('#ddlm').on('input', function(e){
    if ($('#ddlm').val() < 1){
        alert("Минимальное значение - 1!");
        $('#ddlm').val(1);
    } else {
        let z = $('#ddlm').val();
        if (z >= 11 & z < 20){
            $('.ddlm').text("Месяцев");
        }
        else if (z == 1 || z % 10 == 1){
            $('.ddlm').text("Месяц");
        } 
        else if( z % 10 > 1 & z % 10 <= 4) $('.ddlm').text("Месяца");
        else{
            $('.ddlm').text("Месяцев")
        }
    }
});


//  Авторизация 

