var isIE6 = !!($.browser.msie && $.browser.version == '6.0');
function fixPng() {            
    if(isIE6) {
        var fixFn = function() {
            if(this.tagName == 'IMG') {
                var $img = $('<span></span>').css({
                    width: this.offsetWidth,
                    height: this.offsetHeight,
                    display: 'inline-block'
                });
                $img[0].style.filter = 'progid:DXImageTransform.Microsoft.AlphaImageLoader(src="'+this.src+'", sizingMethod="crop")';                        
                $(this).replaceWith($img);
            }
        }
        this.each(function(){
            if(this.complete){
                fixFn.call(this);
            } else {
                this.onload = fixFn ;
            }
        });
    }
    return this;
}