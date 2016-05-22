var view = {
    init: function() {
        $(".categorieBtn").click(function () {
            console.log("click");
            $(".categorieBtn").removeClass("active");
            $(this).addClass("active");
            console.log(this);
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