/**
* Namespace
*/

var Shabby = Shabby || {};

/**
* Initalization for page load
*/

Shabby.init = (function() {
	// Items waiting for DOM load
	$j(document).ready(function() {
		
		/**
		* Function to fire at load
		*/
		Shabby.revealSearch();
		Shabby.dropdown();
        Shabby.dropdownInner();
		Shabby.ajaxCart();
		Shabby.overlay();
		Shabby.storeSelect();
		Shabby.fabrics();
		Shabby.emailModal();
		Shabby.fixFloat();
		Shabby.blogSidebar();
		//Shabby.blogMenu();
		Shabby.checkoutPo();
        Shabby.largeImage();
        Shabby.lookBook();
        Shabby.touchSupport();
        Shabby.largeImage();
        Shabby.blogArchive();
		Shabby.layeredMenu();


	});
})();


/**
* Reveals search form on search icon click
*/

Shabby.revealSearch = function() {
	var $trigger = $j('.reveal-search'),
		$target	= $j('.form-search'),
		$mobileTrigger = $j('.show-search'),
		$mobileTarget = $j('.mobile-search .form-search');
	
	$trigger.one('click', function(e) {
		e.preventDefault();
		$trigger.hide();
		$target.show();
	});
	
	$mobileTrigger.click(function() {
		$mobileTarget.toggle();
	});
}

/**
* Main menu dropdown functionality
*/

Shabby.dropdown = function() {

	// Add events for displaying
	var trigger		= 'li.has-dropdown',
		nav			= $j('.main.nav-section'),
		navOffset	= nav.offset(),
		navOffsetLeft = navOffset.left,
		navOffsetRight = navOffset.left + nav.outerWidth();
	
	$j('.site-nav ' + trigger).on({
		mouseenter: function() {
			$j(this).addClass('hover');
		},
		mouseleave: function() {
			$j(this).removeClass('hover');
		}
	});
	
	// Add last class to menu columns
	$j('.dropdown .menu-column:last-child').addClass('last');
	
	// Dynamic dropdown styling
	$j('.nav-section .dropdown').each(function() {
		
		// Calculate widths of columns
		var count = $j(this).find('.menu-column').length,
			width = $j('.menu-column').outerWidth(),
			dropdownWidth = count * width,
			parentLi = $j(this).parents('li.level-top'),
			dropdownOffset = parentLi.offset(),
			dropdownOffsetLeft = dropdownOffset.left;
			
		$j(this).width(dropdownWidth);
		
		// Just make sure that any uneven float elements don't create oversized widths
		if($j(this).outerWidth() > nav.outerWidth()) {
			$j(this).outerWidth(nav.outerWidth()) 
		}
		
		dropdownWidth = $j(this).outerWidth();
		
		/**
		* Calculate right offset of dropdowns
		* Uses parent LI for left offset since offsets 
		* don't work on hidden elements
		*/
		
		var dropdownOffsetRight = dropdownOffset.left + dropdownWidth;
		
		// Calculate offsets
		if (dropdownOffsetRight > navOffsetRight) {
			var newOffset = dropdownOffsetRight - navOffsetRight;
			$j(this).css({'left' : '-' + newOffset + 'px'});	
		}
	});
	
	// Mobile menu toggle
	$j('.mobile-menu').click(function() {
		if (nav.hasClass('main-hide')) {
			nav.removeClass('main-hide');
		} else {
			nav.addClass('main-hide');	
		}
	});
}

Shabby.dropdownInner = function() {

    if($j('.block-layered-nav').length) {
	    // Add events for displaying
	    var trigger		= '.quick-filter.has-dropdown',
	    	nav			= $j('.block-layered-nav'),
	    	navOffset	= nav.offset(),
	    	navOffsetLeft = navOffset.left,
	    	navOffsetRight = navOffset.left + nav.outerWidth();
	    
	    $j('.site-nav ' + trigger).on({
	    	mouseenter: function() {
	    		$j(this).addClass('hover');
	    	},
	    	mouseleave: function() {
	    		$j(this).removeClass('hover');
	    	}
	    });
	    
	    // Add last class to menu columns
	    $j('.block-layered-nav .dropdown .menu-column:last-child').addClass('last');
	    
	    // Dynamic dropdown styling
	    $j('.block-layered-nav .dropdown').each(function() {
	    	
	    	// Calculate widths of columns
	    	var count = $j(this).find('.menu-column').length,
	    		width = $j('.menu-column').outerWidth(),
	    		dropdownWidth = count * width,
	    		parentLi = $j(this).parents('.menu-column'),
	    		dropdownOffset = parentLi.offset(),
	    		dropdownOffsetLeft = dropdownOffset.left;
	    		
	    	$j(this).width(dropdownWidth);
	    	
	    	// Just make sure that any uneven float elements don't create oversized widths
	    	if($j(this).outerWidth() > nav.outerWidth()) {
	    		$j(this).outerWidth(nav.outerWidth()) 
	    	}
	    	
	    	dropdownWidth = $j(this).outerWidth();
	    	
	    	/**
	    	* Calculate right offset of dropdowns
	    	* Uses parent LI for left offset since offsets 
	    	* don't work on hidden elements
	    	*/
	    	
	    	var dropdownOffsetRight = dropdownOffset.left + dropdownWidth;
	    	
	    	// Calculate offsets
	    	if (dropdownOffsetRight > navOffsetRight) {
	    		var newOffset = dropdownOffsetRight - navOffsetRight;
	    		$j(this).css({'left' : '-' + newOffset + 'px'});	
	    	}
	    });
	    
	    // Mobile menu toggle
	    $j('.quick-filter').click(function() {
	    	if (nav.hasClass('main-hide')) {
	    		nav.removeClass('main-hide');
	    	} else {
	    		nav.addClass('main-hide');	
	    	}
	    });
    }
}


/**
* Mini cart in header
*/

Shabby.ajaxCart = function() {
	var cart 	= $j('.block-cart'),
		trigger	= $j('.top-link-cart');
		
	// Reveal or hide on hover
	trigger.hover(function(e) {
		cart.addClass('reveal');
	},
	function(e) {
		var target = e.toElement || e.relatedTarget;
		
		if (target.className == "block block-cart reveal" || target.className == "cart-arrow") {
			return false;	
		}
		
		cart.removeClass('reveal');
	});
	
	// Catch for hiding from mouse out of cart
	$j(document).on('mouseleave', '.block-cart', function() {
		cart.removeClass('reveal');
	});
}

/**
* Share button
*/

Shabby.share = function() {
	var target 	= $j('.share'),
		trigger	= $j('.reveal-share');
    
	// Reveal or hide on hover
	trigger.hover(function(e) {
        target.stop().fadeIn('fast');
		//target.addClass('reveal');
	},
	function(e) {
		var targetEl = e.toElement || e.relatedTarget;
		
		if (targetEl.className == "share reveal" || targetEl.className == "arrow") {
			return false;	
		}
		
		//target.removeClass('reveal');
        target.fadeOut('fast');
	});
	
	// Catch for hiding from mouse out of cart
	$j(document).on('mouseleave', '.share', function() {
		//target.removeClass('reveal');
        target.fadeOut('fast');
	});
}

/**
* Image overlays
*/

Shabby.overlay = function() {

	var overlay = $j('.overlay-block.full'),
			main	= $j('.camera_wrap');
		
	if (main) {
		var mainHeight = main.height();
		
		$j('.camera_wrap .overlay-block').each(function() {
			$j(this).height(mainHeight);
		});
	}
		
	// Wait for images to load
	$j(window).load(function() {
	
		if (overlay) {
			overlay.each(function() {
				var height = $j(this).height();
				$j(this).find('.overlay').height(height);
			});
		}
		
		// Attach hover event
		$j(document).on('hover', '.overlay', function() {
			$j(this).trigger('hover');
		});
		
		$j(window).resize(function() {
			if (overlay) {
				overlay.each(function() {
					if($j(this).parents('.main-banner').length == 0) {
						height = $j(this).height();
						$j(this).find('.overlay').height(height);
					}
				});
			}
			if (main) {
				var mainHeight = main.height();
				
				$j('.camera_wrap .overlay-block').each(function() {
					$j(this).height(mainHeight);
				});
			}
		});
		
	});
}

/**
* Custom Select
*/

Shabby.customSelect = function() {
	
	if($j('.catalog-product-view').length != -1) {
		$$('select').each(function(select) {
			new Chosen(select, {disable_search: true});
		});
		
		$$('select').invoke('observe', 'change', function() {
			$$('select').each(function(el){
				Event.fire(el, "chosen:updated");
			});
		});
		
		$$('.chosen-container').invoke('observe', 'mouseenter', function() {
			$$('select').each(function(el){
				Event.fire(el, "chosen:updated");
			});
		});
		
		Ajax.Responders.register({
			onComplete : function(){
				$$('select').each(function(el){
					Event.fire(el, "chosen:updated");
				});
			}
		});
	
	}
}


/**
* Accordion Menu
*/

Shabby.accordion = function(trigger, target) {
	var $trigger	= $j(trigger),
		$target		= $j(target);
		
	$j(document).on('click', trigger, function() {
		var that = $j(this),
			children = that.children('span');
		
		that.siblings(target).toggle('fast', function() {
			if (children.hasClass('closed')) {
				children.removeClass('closed').addClass('open');
				children.html('-');
			} else {
				children.removeClass('open').addClass('closed');
				children.html('+');
			}
		});
	});
}

/**
* Store Select
*/

Shabby.storeSelect = function() {
	var selector = $j('.store-select');
	
	selector.change(function(e){
		window.location.href = '/stores#' + selector.val();
	});
}

/**
* Display archives
*/

Shabby.blogArchive = function() {
	$j(document).on('click', '.archive-title', function() {
        if($j('#wp-archive-list').hasClass('activeList')) {
            $j('#wp-archive-list').removeClass('activeList');
            $j('#wp-archive-list').fadeOut();
        } else {
            $j('#wp-archive-list').addClass('activeList');
            $j('#wp-archive-list').fadeIn();
        }
	});
}

/**
* Fabric Viewer
*/

Shabby.fabrics = function() {
    
	var fabricList = $j('.fabric-list');
	
	fabricList.on('click', '.show-details', function(e) {
		e.preventDefault();
		
		var link = $j(this).attr('href');
		
		$j('.mini-detail').each(function() {
			$j(this).css({'display':'none'});
		});
		
		$j(link).css({'display':'block'});
	});
    
    // Open div that is the same width and height of product-shop
    $j(document).on('click', '.fabric-open', function(e) {
		e.preventDefault();

        var elHeight = $j('.product-shop').outerHeight();
        var elWidth = $j('.product-shop').outerWidth();
        $j('.fabrics-expanded').css({minHeight: elHeight, minWidth: elWidth });
		$j('.fabrics-expanded').fadeIn();
        
	});
    
    // Close
    $j(document).on('click', '.close', function(e) {
		e.preventDefault();
        $j('.fabrics-expanded').fadeOut();
    });
    
    // Close container if clicked elsewhere
    jQuery(document).on('mouseup', function (e) {
        
        var container = $j('.fabrics-expanded');
        
        if(container.is(":visible") ) {
		
			if (!container.is(e.target) && container.has(e.target).length === 0) {
				container.fadeOut();
			}
            
        }
    });
    
    
}

/**
* Email Modal
*/

Shabby.emailModal = function() {
	var modal = $j('#emailSignup'),
		cookie = $j.cookie('returningVisitor');
	
	if(cookie === undefined) {
		$j.cookie('returningVisitor', 'true', {expires: 365});
		
		modal.foundation('reveal', 'open');
	}
	
	$j('.close-reveal-modal', modal).click(function(e){
		e.preventDefault();
		
		modal.foundation('reveal', 'close');
	});
}

/**
* Add Foundation end tag
*/

Shabby.fixFloat = function() {
	var target = $j('.products-grid, .subcategory-grid');
	
	target.each(function() {
		if ($j(this).children('.item').length < 3) {
			
			$j('.item', this).last().addClass('end');
		}
	});
}

/**
* Font Large image selector
*/

Shabby.largeImage = function() {
	
	$j('.swatch :first-child').click(function() {

        $j('.swatch a.image-large').hide();
        $j(this).next().show();
    });
}


/**
* Allow Blog Sidebar to be shown after blog content
*/

Shabby.blogSidebar = function() {
	var target = $j('.is-blog .sidebar').parent();
	
	if ($j('.main-hide').css('display') == 'none') {
		$j('.is-blog .col-main').parent().insertBefore(target);
	}
}

/**
* Blog Menu adjustment so it appears next to "Shop" link
*/

Shabby.blogMenu = function() {
	var target = $j('#static-menu ul li:first a');
	
	if ($j('.main-hide').css('display') == 'none') {
		target.text('Blog');
		target.appendTo('.mobile-menu');
	}
}

/**
* Check if the customer is entering a PO box
*/

Shabby.checkoutPo = function() {
	
	$j('#billing\\:street1, #billing\\:street2').keyup(function() {
		var pattern = new RegExp('\\b[p]*(ost)*\\.*\\s*[o|0]*(ffice)*\\.*\\s*b[o|0]x\\b', 'i');
		if($j(this).val().match(pattern) && $j('#shipping\\:same_as_billing').is(':checked')) {
			alert('We currently do not ship to PO Boxes.');
		}
	});

	$j('#shipping\\:same_as_billing').on('change', function() {
		
		if($j('#shipping\\:same_as_billing').is(':checked')) {
			var target = $j('#billing\\:street1, #billing\\:street2');
		} else {
			var target = $j('#shipping\\:street1, #shipping\\:street2');
		}
		
		target.keypress(function() {
			var pattern = new RegExp('\\b[p]*(ost)*\\.*\\s*[o|0]*(ffice)*\\.*\\s*b[o|0]x\\b', 'i');
			if($j(this).val().match(pattern)) {
				alert('We currently do not ship to PO Boxes.');
			}
		});
		
	});
}

/**
* Lookbook select year
*/

Shabby.lookBook = function() {
	var path = window.location.pathname.split('/')[1];	

	$j('.lookbook-select').change(function() {
		var url = $j(this).val();

		window.location = url;
	});
}

/**
* Touch support
*/

Shabby.touchSupport = function() {
	var supportsTouch = 'ontouchstart' in window || navigator.msMaxTouchPoints;

	if (supportsTouch) {
		jQuery('.subcategory-grid .subcat-overlay').addClass('show-touch');
	}
}

/**
 * Category page layered menu
 */

Shabby.layeredMenu = function() {
	var $menu = jQuery('.subcat-parent');

	$menu.each(function() {
		var $that = jQuery(this);
		var height = jQuery('> a', this).height();

		$that.data('closeHeight', height);
		$that.data('openHeight', $that.height());

		if ($that.hasClass('active')) {
			$that.css({
				'overflow': 'hidden',
				'height': $that.data('openHeight')
			});
		} else {
			$that.css({
				'overflow': 'hidden',
				'height': $that.data('closeHeight')
			});
		}

	});

	jQuery('.has-subcats', $menu).click(function(e) {
		e.preventDefault();
		var $parentLi = jQuery(this).closest('.subcat-parent');

		if ($parentLi.hasClass('active')) {
			$parentLi.removeClass('active');
			$parentLi.animate({'height': $parentLi.data('closeHeight')}, 'slow');
		} else {
			$parentLi.addClass('active');
			$parentLi.animate({'height': $parentLi.data('openHeight')}, 'slow');
		}

	});
}
