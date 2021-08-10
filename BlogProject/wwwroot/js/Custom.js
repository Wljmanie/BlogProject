
let index = 0;





function AddTag() {
    var tagEntry = document.getElementById("TagEntry");


    let searchResult = Search(tagEntry.value);

    if (searchResult != null) {
        swalWithDarkButton.fire({
            html: `<span class="font-weight-bolder">${searchResult}</span>`

        });
    }
    else {
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
        
    }



    tagEntry.value = "";
    return true;


}

function DeleteTag() {
    let counter = 1;
    let tagList = document.getElementById("TagList");

    if (!tagList) return false;
    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: '<span class="font-weight-bolder">Choose a Tag before deleting.</span>'
        });
        return true;
    }

    while (counter > 0) {
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {

            tagList.options[selectedIndex] = null;
           
        } else {
            counter = 0;
        index--;
        }
    }
    


}




$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let i = 0; i < tagArray.length; i++) {
        ReplaceTag(tagArray[i], i);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

function Search(str) {
    if (str == "") {
        return "Empty tags are not permitted."
    }

    let tagElement = document.getElementById("TagList");

    if (tagElement) {
        let options = tagElement.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `The taag #${str} was detected as a duplicated tag. Those are not permitted.`
            }
        }



    }



}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-outline-dark'
    },
    timer: 3000,
    buttonsStyling: false
    });





