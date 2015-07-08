
	

// Search in header
function loadSearchTextURL() {
    var searchText = $('#searchTextfieldId').val();
    window.open('http://www.maryland.gov/pages/search.aspx?q=' + searchText, '_blank')
}

jQuery(function ($) {

    $('.horizontal-menu > li > a').click(function () {
        $('.horizontal-menu > li').removeClass('open');
        $(this).parent.addClass('open');
    });

// Left navigation toggle menu for ipad and mobile view
	$(".toggle-quick-links").click(function() {
		$(this).toggleClass('arrow-down');
		$(this).next('.links-list').toggleClass('toggle-view');
	});
	
	// Travel Information - Travel links toggle menu for ipad and mobile view
	$('.toggle-travel-links').click(function(){
		$(this).toggleClass('arrow-down');
		$(this).next('.travel-links-list').toggleClass('toggle-view');
	});



    $('a[href="#"]').on('click', function (e) {
        e.preventDefault();
    });
    $('#menu > li > ul').hide();
    $('#menu > li').on('mouseover', function (e) {
        $(this).find("ul:first").show();
        $(this).find('> a').addClass('active');
    }).on('mouseout', function (e) {
        $(this).find("ul:first").hide();
        $(this).find('> a').removeClass('active');
    });


    //#main-slider
    $(function () {
        $('#main-slider.carousel').carousel({
            interval: 5000
        });
    });

    $('.centered').each(function (e) {
        $(this).css('margin-top', ($('#main-slider').height() - $(this).height()) / 2);
    });

    $(window).resize(function () {
        $('.centered').each(function (e) {
            $(this).css('margin-top', ($('#main-slider').height() - $(this).height()) / 2);
        });
    });

    //contact form
    var form = $('.contact-form');
    form.submit(function () {
        $this = $(this);
        $.post($(this).attr('action'), function (data) {
            $this.prev().text(data.message).fadeIn().delay(3000).fadeOut();
        }, 'json');
        return false;
    });

    //goto top
    $('.gototop').click(function (event) {
        event.preventDefault();
        $('html, body').animate({
            scrollTop: $("body").offset().top
        }, 500);
    });

    // Dropdown hover
    $('.dropdown-toggle[href="#"]').dropdown();

    $('.dropdown-toggle').click(function(e) {
        var url = $(this).attr('href');
        window.location = url;
        e.preventDefault();
    });

});

