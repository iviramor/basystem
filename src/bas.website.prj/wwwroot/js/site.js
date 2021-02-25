

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