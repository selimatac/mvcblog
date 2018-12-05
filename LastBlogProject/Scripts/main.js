$(document).ready(function () {
    $('ul.pagination li').addClass('page-item');
    $('ul.pagination li a').addClass('page-link');

    $(".sticky_sidebar").stick_in_parent(); 
    CKEDITOR.replace('editor1').config.allowedContent = true;

})