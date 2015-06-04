
// A floating back to top button
var amountScrolled = 300;

$(window).scroll(function () {
    if ($(window).scrollTop() > amountScrolled) {
        $('a.back-to-top').fadeIn('slow');
    } else {
        $('a.back-to-top').fadeOut('slow');
    }
});

$('a.back-to-top').click(function () {
    $('html,body').animate({
        scrollTop: 0
    }, 700);
    return false;
});


$(document).ready(function () {
    // Init magnific popups
    $('.open-popup-link').magnificPopup({
        type: 'inline',
        midClick: true // Allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source in href.
    });
});
