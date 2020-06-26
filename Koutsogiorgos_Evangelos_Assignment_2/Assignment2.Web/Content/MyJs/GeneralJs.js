(function () {
    document.getElementById("pageSize").addEventListener("change", change)
})();

function change() {
    document.getElementById("form").submit();
}