$(document).ready(function() {
  $("#search").click(function() {
    $(".result-table").toggleClass("display-none");
  });
  $(".xzoom, .xzoom-gallery").xzoom({ tint: "#333", Xoffset: 15 });
  $('.drawer').drawer();
});
function onRegisterClick(e) {
  e.preventDefault();

  var form = $("#register-form");
  var timer = $(".timer");
  var counter = $(".counter");
  var confirm_number_form = $("#confirm-number-form");

  form.hide();
  timer.show();
  confirm_number_form.show();

  var count = 60;
  var interval = setInterval(function() {
    count = --count;
    counter.text(count);
    if (count == 0) {
      clearInterval(interval);
    }
  }, 1000);
}
function onLoginClick(e) {
  e.preventDefault();

  var form = $("#login-form");
  var timer = $(".timer");
  var counter = $(".counter");
  var confirm_number_form = $("#confirm-number-form");

  form.hide();
  timer.show();
  confirm_number_form.show();

  var count = 60;
  var interval = setInterval(function() {
    count = --count;
    counter.text(count);
    if (count == 0) {
      clearInterval(interval);
    }
  }, 1000);
}
