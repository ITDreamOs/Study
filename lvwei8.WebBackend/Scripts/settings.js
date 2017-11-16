var site_settings = '<div class="ts-button">'
        +'<span class="fa fa-cog fa-spin"></span>'
    +'</div>'
    +'<div class="ts-body">'
        +'<div class="ts-title">偏好设置</div>'
        +'<div class="ts-row">'
            +'<label class="ui-checkbox"><input type="checkbox" name="st_head_fixed" value="1"/> <span>置顶头部</span></label>'
        +'</div>'
        +'<div class="ts-row">'
            +'<label class="ui-checkbox"><input type="checkbox" name="st_sb_fixed" value="1"/> <span>置顶菜单</span></label>'
        +'</div>'
        +'<div class="ts-title">菜单设置</div>'
        +'<div class="ts-row">'
            +'<label class="ui-radio"><input type="radio" name="st_sb_pos" value="1"/> <span>横排显示</span></label>'
        +'</div>'
        +'<div class="ts-row">'
            +'<label class="ui-radio"><input type="radio" name="st_sb_pos" value="0"/> <span>左侧显示</span></label>'
        +'</div>'
        +'<div class="ts-title">全局设置</div>'
        +'<div class="ts-row">'
            +'<label class="ui-radio"><input type="radio" name="st_layout_boxed" value="0"/> <span>铺满屏幕</span></label>'
        +'</div>'
        +'<div class="ts-row">'
            +'<label class="ui-radio"><input type="radio" name="st_layout_boxed" value="1"/> <span>居中显示</span></label>'
        +'</div>'
    +'</div>';
    
var settings_block = document.createElement('div');
    settings_block.className = "theme-settings";
    settings_block.innerHTML = site_settings;
    document.body.appendChild(settings_block);

$(document).ready(function(){
    var theme_settings = {
        st_head_fixed: getCookie("st_head_fixed"),
        st_sb_fixed: getCookie("st_sb_fixed"),
        st_sb_pos: getCookie("st_sb_pos"),
        st_layout_boxed: getCookie("st_layout_boxed")
    };
    
    set_settings(theme_settings,false);

    $(".theme-settings input").on("click", function(e) {
        var input   = $(this);
        if(input.attr("name") != 'st_layout_boxed'){
            if(!input.prop("checked")){
                theme_settings[input.attr("name")] = input.val();
            }else{            
                theme_settings[input.attr("name")] = 0;
            }
            
        }else{
            theme_settings[input.attr("name")] = input.val();
        }

        if(input.attr("name") === 'st_sb_pos'){
            theme_settings.st_sb_pos = input.val();
            setCookie("st_sb_pos",theme_settings.st_sb_pos);
        }

        if(input.attr("name") === 'st_head_fixed'){
            if(theme_settings.st_layout_boxed == 1){                
                theme_settings.st_head_fixed    = -1;
                theme_settings.st_sb_fixed      = -1;
            }else{
                if(theme_settings.st_head_fixed == 1){
                    theme_settings.st_head_fixed = 0;
                    theme_settings.st_sb_fixed = -1;
                }else{
                    theme_settings.st_head_fixed = 1;
                    theme_settings.st_sb_fixed = 0;
                }
            }
            setCookie("st_head_fixed",theme_settings.st_head_fixed);
            setCookie("st_sb_fixed",theme_settings.st_sb_fixed);
        }

        if(input.attr("name") === 'st_sb_fixed'){
            if(theme_settings.st_layout_boxed == 1){                
                theme_settings.st_head_fixed    = -1;
                theme_settings.st_sb_fixed      = -1;
            }else{
                if(theme_settings.st_sb_fixed == 1){
                    theme_settings.st_sb_fixed = 0;
                }else{
                    theme_settings.st_sb_fixed = 1;
                    theme_settings.st_head_fixed = 1;
                }
            }
            setCookie("st_head_fixed",theme_settings.st_head_fixed);
            setCookie("st_sb_fixed",theme_settings.st_sb_fixed);
        }

        if(input.attr("name") === 'st_layout_boxed'){
            if(theme_settings.st_layout_boxed == 1){                
                theme_settings.st_head_fixed    = -1;
                theme_settings.st_sb_fixed      = -1;
            }else{
                theme_settings.st_head_fixed    = 1;
                theme_settings.st_sb_fixed      = 1;
            }
            setCookie("st_layout_boxed",theme_settings.st_layout_boxed);
            setCookie("st_head_fixed",theme_settings.st_head_fixed);
            setCookie("st_sb_fixed",theme_settings.st_sb_fixed);
        }
        
        set_settings(theme_settings,input.attr("name"));
    });
    
    /* Open/Hide Settings */
    $(".ts-button").on("click",function(){
        $(".theme-settings").toggleClass("active");
    });
    /* End open/hide settings */
});

function set_settings(theme_settings,option){
    if(theme_settings.st_head_fixed == 1){
        $(".topbar").addClass("topbar-fixed");
    }else{
        $(".topbar").removeClass("topbar-fixed");
    }   
   
    if(theme_settings.st_sb_fixed == 1){        
        $(".sidebar").addClass("sidebar-fixed");
    }else
        $(".sidebar").removeClass("sidebar-fixed");
 
    if(theme_settings.st_sb_pos == 1){
        $("body").addClass("top-bar-horizontal");
        $(".sidebar").addClass("sidebar-horizontal");
    }else{
        $("body").removeClass("top-bar-horizontal");
        $(".sidebar").removeClass("sidebar-horizontal");
    }
    
    if(theme_settings.st_layout_boxed == 1)
        $("body").addClass("layout-boxed");
    else
        $("body").removeClass("layout-boxed");
    
    /* Set states for options */
    if(option === false || option === 'st_layout_boxed' || option === 'st_head_fixed' || option === 'st_sb_fixed'){        
        for(option in theme_settings){
            set_settings_checkbox(option,theme_settings[option]);
        }
    }
    
    $(window).resize();
}

function set_settings_checkbox(name,value){
    
    if(name == 'st_layout_boxed'){    
        
        $(".theme-settings").find("input[name="+name+"]").prop("checked",false);
        
        var input = $(".theme-settings").find("input[name="+name+"][value="+value+"]");
                
        input.prop("checked",true);      
        
    }else{
        
        var input = $(".theme-settings").find("input[name="+name+"]");
        
        input.prop("disabled",false);      
        
        if(value === 1){
            input.prop("checked",true);
        }
        if(value === 0){
            input.prop("checked",false);                        
        }
        if(value === -1){
            input.prop("checked",false);            
            input.prop("disabled",true);
        }        
                
    }
}