// On Load
$(document).ready(function () {
    // prepare collapsible elements
    $('.toggleHead').prepend('<span class="toggleIcon collapsed"></span><span class="toggleText">').append('</span>').click(function () { $(this).toggleCollapse(); });

    $(document).foundationNavigation();

    $('.loader').click(function () {
        $('#loading').show();
    });
});

// jQuery Helper Functions
(function ($) {
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $.fn.rsInit = function () {
        // initialize slider
        $(this).royalSlider({
            controlNavigation: 'thumbnails',
            allowCSS3OnMacWebkit: true,
            arrowsNav: false,
            fullscreen: false,
            loop: true,
            thumbs: {
                fitInViewport: true,
                firstMargin: false,
                paddingBottom: 0
            },
            autoPlay: {
                enabled: false
            },
            numImagesToPreload: 0,
            thumbsFirstMargin: false,
            autoScaleSlider: true,
            autoScaleSliderWidth: 960,
            autoScaleSliderHeight: 600,
            keyboardNavEnabled: true,
            navigateByClick: false,
            fadeinLoadedSlide: true,
            deeplinking: {
                enabled: true,
                change: true
            }
        }).rsUpdate().data('royalSlider').ev.on('rsAfterSlideChange', function (event) {
            $(this).rsUpdate();
        });
        return this;
    }

    $.fn.rsUpdate = function () {
        var newHeight = $('.rsContent').css('height');
        $(this).css('height', newHeight);
        $(this).css('height', '+=100');
        if($(this).data('royalSlider') != null)
            $(this).data('royalSlider').updateSliderSize();
        return this;
    }

    $.fn.loader = function () {
        $(this).html('<img src="/Content/images/loading100.gif" alt="loading..." class="loader">');
        return this;
    }

    $.fn.goTo = function () {
        $('html, body').animate({
            scrollTop: $(this).offset().top + 'px'
        }, 'fast');
        return this; // for chaining...
    }

    $.fn.startLoad = function () {
        $(this).html('');
        $('#loading').show();
        return this;
    }

    $.fn.finLoad = function () {
        $('#loading').hide();
        return this;
    }

    $.fn.loadContent = function (action, param) {
        $(this).startLoad().load(action + (param != undefined ? "/" + param : ""), function () {
            $(this).finLoad().rsUpdate();
        });
        return this;
    }
    
    $.fn.toggleCollapse = function () {
        if($(this).parent().next('div.toggleContent').css('display')==='none'){
            $(this).find('span.expanded').remove();
            $(this).prepend('<span class="toggleIcon collapsed"></span>');
        }else{
            $(this).find('span.collapsed').remove();
            $(this).prepend('<span class="toggleIcon expanded"></span>');
        }
        $(this).parent().next('div.toggleContent').slideToggle();
        return this;
    }

    $.fn.placeholder = function () {
        // do stuff
        return this;
    }
})(jQuery);