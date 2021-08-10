
let index = 0;





function AddTag() {
    var tagEntry = document.getElementById("TagEntry");

    let newOption = new Option(tagEntry.value, tagEntry.value);
    document.getElementById("TagList").options[index++] = newOption;

    tagEntry.value = "";
    return true;


}

function DeleteTag() {

        let tagList = document.getElementById("TagList");
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            tagList.options[selectedIndex] = null;
           
        }
        index--;
    


}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})




