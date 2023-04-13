//function navbar() {
//    const sidebar = document.querySelector('.hamburger-menu');
//    sidebar.classList.toggle('show-menu');
//    if (sidebar.classList.contains('show-menu')) {
//        sidebar.style.height = '100vh';
//        sidebar.style.width = '300px';
//        sidebar.style.position = 'absolute';
//        sidebar.style.zIndex = '9999';
//    } else {
//        sidebar.style.height = '';
//        sidebar.style.width = '';
//        sidebar.style.position = '';
//        sidebar.style.zIndex = '';
//    }
//}

function navbar() {
    document.querySelector('.hamburger-menu').classList.toggle('show-menu');
}
