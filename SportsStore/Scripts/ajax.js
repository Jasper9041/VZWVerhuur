var view = {
    init: function() {
        $(".categorieBtn").click(function () {
            $(".categorieBtn").removeClass("active");
          $(this).addClass("active");
            $.post(this.action, $(this).serialize(),
                function(data) {
                    $("#producten").html(data);
                });
        });
    }
}
$(function () {
    view.init();
   
});