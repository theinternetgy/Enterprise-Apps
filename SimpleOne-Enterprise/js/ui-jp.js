+function ($) {

  $(function(){
      initui();
      
  });
}(jQuery);
function initui(){
    $("[ui-jq]").each(function(){
        var self = $(this);
        var options = eval('[' + self.attr('ui-options') + ']');

        if ($.isPlainObject(options[0])) {
          options[0] = $.extend({}, options[0]);
        }

        uiLoad.load(jp_config[self.attr('ui-jq')]).then( function(){          
          self[self.attr('ui-jq')].apply(self, options);
        });
      });
}
var initChosen = function () {
    $('.chosen-select').chosen();
}