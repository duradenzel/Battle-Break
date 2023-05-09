$('.hamburgerNavigator').on('click', function () {
    console.log("Clicked hamburger navigator"); // log a message to the console
    $('.sidebar').toggleClass('reveal'); // Toggle reveal class on sidebar
});