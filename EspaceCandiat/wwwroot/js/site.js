$(document).ready(function () {
    const form = document.getElementById("ApplyFromStep");

    const validateStep1 = () => {
        const fields = ["nom", "prenom", "email", "tel", "etude", "experience", "employeur"];
        for (let i = 0; i < fields.length; i++) {
            const field = form[fields[i]];
            if (!field || !field.value) {
                alert(`Veuillez remplir le champ ${fields[i]}`);
                return false;
            }
        }
        return true;
    };

    const validateStep2 = () => {
        const cv = document.getElementById("cv");
       //console.log();
        if (!cv || !cv.files || !cv.files.length) {
            alert("Veuillez télécharger un CV.");
            return false;
        }
        const fileType = cv.files[0].type;
        const allowedFileTypes = ["application/pdf", "image/jpeg", "image/png"];
        if (!allowedFileTypes.includes(fileType)) {
            alert("Le format du CV n'est pas valide. Veuillez télécharger un CV au format PDF ou image (JPEG/PNG).");
            return false;
        }

        return true;
    };


    const navigateToFormStep = (stepNumber) => {
        if (stepNumber === 2 && !validateStep1()) {
            return;
        } else if (stepNumber === 3 && !validateStep2()) {
            return;
        }

        const formSteps = document.querySelectorAll(".form-step");
        const formStepHeaders = document.querySelectorAll(".form-stepper-list");

        formSteps.forEach((formStepElement) => {
            formStepElement.classList.add("d-none");
        });

        formStepHeaders.forEach((formStepHeader) => {
            formStepHeader.classList.add("form-stepper-unfinished");
            if (formStepHeader) {
                formStepHeader.classList.remove("form-stepper-unfinished", "form-stepper-active");
            }
        });

        const currentFormStep = document.querySelector(`#step-${stepNumber}`);
        currentFormStep.classList.remove("d-none");

        const currentFormStepHeader = document.querySelector(`li[step="${stepNumber}"]`);
        currentFormStepHeader.classList.remove("form-stepper-unfinished", "form-stepper-completed");
        currentFormStepHeader.classList.add("form-stepper-active");

        for (let index = 1; index < stepNumber; index++) {
            const formStepHeader = document.querySelector(`li[step="${index }"]`);
            if (formStepHeader) {
                formStepHeader.classList.remove("form-stepper-unfinished", "form-stepper-active");
                formStepHeader.classList.add("form-stepper-completed");
            }
        }

    };

    const formNavigationButtons = document.querySelectorAll(".btn-navigate-form-step");
    formNavigationButtons.forEach((formNavigationButton) => {
        formNavigationButton.addEventListener("click", (event) => {
            const stepNumber = parseInt(event.target.getAttribute("step_number"));
            navigateToFormStep(stepNumber);
        });
    });

//pour envoyer le form
var submitForm = document.getElementById('submitForm');
if (submitForm) {
    submitForm.addEventListener('click', function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('ApplyFromStep'));

    fetch('/Public/AjouterCand', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            let messageDiv = document.getElementById('message');
            if (data.success) {
                messageDiv.innerText = 'Votre candidature soumise avec succès et un message de confirmation et envoyer a votre e-mail';
            } else {
                messageDiv.innerText = 'Erreur lors de la soumission de la candidature';
            }
        })
        .catch((error) => {
            console.error('Error:', error);
            document.getElementById('message').innerText = 'Une erreur est survenue lors de la soumission de la candidature';
        });
});
}

/**********************************
 * admin
 ++++++++++++++++++++++++++++++++*/
$('.delete-btn').on('click', function (e) {
    var candidatId = $(this).data('id');
    var button = $(this);

    $('#confirmDeleteButton').data('id', candidatId);
    $('#confirmDeleteModal').modal('show');
});

$('#confirmDeleteButton').on('click', function (e) {
    var candidatId = $(this).data('id');
    var button = $(this);

    $.ajax({
        url: '/Admin/Delete/' + candidatId,
        type: 'POST',
        success: function (result) {
            $('button[data-id="' + candidatId + '"]').closest('tr').remove();
            $('#confirmDeleteModal').modal('hide');
        }
    });
});
    $('body').on('click', '[data-bs-toggle="modal"]', function (e) {
        var candidatId = $(this).data('candidat-id');

        var cvUrl = '/Admin/GetCV/' + candidatId + '?t=' + new Date().getTime();

        // Remove the old object, if any
        $('#cvObject').remove();
        var newObject = $('<object>')
            .attr('data', cvUrl)
            .attr('type', 'application/pdf')  
            .attr('width', '100%')
            .attr('height', '600px')
            .attr('id', 'cvObject');
        $('#ModalCV .modal-body').append(newObject);
        $('#cvDownloadLink').attr('href', cvUrl);
        $('#ModalCV').modal('show');
    });

    $('#ModalCV').on('hidden.bs.modal', function (e) {
        $('#cvDownloadLink').attr('href', '');
    });


});