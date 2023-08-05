// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.modelc-toggler').click((event) => {
    debugger;
    var target = $(event.currentTarget).attr('model-target');
    $(target).css('display', 'block');
})

$('.modalc-close').click((event) => {
    $(event.currentTarget).closest('.modalc').css('display', 'none'); //$(model).hide();
})

$('#test').click((event) => {
    console.log($(event.currentTarget).attr('id'))
    /* var target = '#' + event.target.id
    console.log(target)
    var test = $(target).attr('test-attr');
    console.log(test) */
})