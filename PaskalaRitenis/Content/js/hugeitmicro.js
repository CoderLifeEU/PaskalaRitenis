jQuery(function () {
    var defaultBlockWidth = 275 + 20 + 550;
    var $container = jQuery('#huge_it_gallery_container_11');


    // add randomish size classes
    $container.find('.element_11').each(function () {
        var $this = jQuery(this),
            number = parseInt($this.find('.number').text(), 10);
        //alert(number);
        if (number % 7 % 2 === 1) {
            $this.addClass('width2');
        }
        if (number % 3 === 0) {
            $this.addClass('height2');
        }
    });

    $container.hugeitmicro({
        itemSelector: '.element_11',
        masonry: {
            columnWidth: 275 + 10 + 0
        },
        masonryHorizontal: {
            rowHeight: 'auto'
        },
        cellsByRow: {
            columnWidth: 275,
            rowHeight: 'auto'
        },
        cellsByColumn: {
            columnWidth: 275,
            rowHeight: 'auto'
        },
        getSortData: {
            symbol: function ($elem) {
                return $elem.attr('data-symbol');
            },
            category: function ($elem) {
                return $elem.attr('data-category');
            },
            number: function ($elem) {
                return parseInt($elem.find('.number').text(), 10);
            },
            weight: function ($elem) {
                return parseFloat($elem.find('.weight').text().replace(/[\(\)]/g, ''));
            },
            name: function ($elem) {
                return $elem.find('.name').text();
            }
        }
    });


    var $optionSets = jQuery('#huge_it_gallery_options .option-set'),
        $optionLinks = $optionSets.find('a');

    $optionLinks.click(function () {
        var $this = jQuery(this);

        if ($this.hasClass('selected')) {
            return false;
        }
        var $optionSet = $this.parents('.option-set');
        $optionSet.find('.selected').removeClass('selected');
        $this.addClass('selected');


        var options = {},
            key = $optionSet.attr('data-option-key'),
            value = $this.attr('data-option-value');

        value = value === 'false' ? false : value;
        options[key] = value;
        if (key === 'layoutMode' && typeof changeLayoutMode === 'function') {

            changeLayoutMode($this, options)
        } else {

            $container.hugeitmicro(options);
        }

        return false;
    });

    var isHorizontal = false;
    function changeLayoutMode($link, options) {
        var wasHorizontal = isHorizontal;
        isHorizontal = $link.hasClass('horizontal');

        if (wasHorizontal !== isHorizontal) {

            var style = isHorizontal ?
            { height: '100%', width: $container.width() } :
            { width: 'auto' };

            $container.filter(':animated').stop();

            $container.addClass('no-transition').css(style);
            setTimeout(function () {
                $container.removeClass('no-transition').hugeitmicro(options);
            }, 100)
        } else {
            $container.hugeitmicro(options);
        }
    }

    var $sortBy = jQuery('#sort-by');
    jQuery('#shuffle a').click(function () {
        $container.hugeitmicro('shuffle');
        $sortBy.find('.selected').removeClass('selected');
        $sortBy.find('[data-option-value="random"]').addClass('selected');
        return false;
    });

    jQuery(window).load(function () {
        $container.hugeitmicro('reLayout');
    });
});