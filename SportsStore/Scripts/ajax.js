var view = {
    init: function() {
        $(".categorieBtn").click(function (event) {
            $(".categorieBtn").removeClass("active");
            $(this).addClass("active");
            $.post(this.action, {categoryId: event.target.value},
                function(data) {
                    $("#producten").html(data);
                });
        });
    }
}
$(function () {
    view.init();
   
});