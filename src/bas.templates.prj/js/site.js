

$('.currency-button').on('click', function(e){
    $('.currency-button').removeClass('currency-active');
    $('.currency-button').attr('data-currency-active', 'false');
    $(this).addClass('currency-active');
    $(this).attr('data-currency-active', 'true')
});


$('.indiv-percent').on('click', function(e){

    var sel;
    sel = parseInt($('.calc-select').val())

    console.log(typeof sel)

    $(this).parent('.calc-to-block').children('.calc-input').val((9.3 - sel).toFixed(1));
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


    // console.log($(this).parent('.calc-to-block').children('.calc-input'))
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

function GetFdate(){
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



//  Авторизация 


$("#sign-in button").on('click', function(e){
    $("#sign-in").addClass("hide-block");
    $("#acc").removeClass("hide-block");
    $('.indiv-percent').removeAttr("disabled")
});

$("#acc button").on('click', function(e){
    $("#acc").addClass("hide-block");
    $("#sign-in").removeClass("hide-block");
    $('.indiv-percent').attr("disabled", "disabled")
});