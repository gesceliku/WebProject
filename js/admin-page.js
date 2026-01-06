
$doctorsList=$("#doctorsList")
$addBtn=$("#addBtn")
$updateBtn=$("#updateBtn")
$doctorName=$("#doctorName")
$doctorPass=$("#doctorUsername")
$doctorPass=$("#doctorPass")
$doctorClinic=$("#clinicSelect")
$doctorForm=$("#doctorForm")

function loadDoctors(){
    
    $.ajax({
        url: `http://localhost:5288/api/Doctors/GetDoctorByClinic`,
        method: "GET",
        success: function (response) {
          displayDoctors(response);
        },
        error: function (error) {
          console.error(error);
        },
      });
}

function displayDoctors(doctors) {
    $doctorsList.empty();
    doctors.forEach((doctor) => {
      const $row = $("<tr></tr>");
  
      $row.html(`
        <td>${doctor.username}</td>
        <td>${doctor.name}</td>
        <td>${doctor.clinic}</td>
        <td>
          <button class="btn btn-warning btn-sm remove-btn" data-id="${doctor.username}">Remove</button>
          <button class="btn btn-warning btn-sm edit-btn" data-id="${doctor.username}">Edit</button>
        </td>
      `);
  
      $airlinesList.append($row);
    });
  }

  function deleteDoctor(username) {
    $.ajax({
      url: `http://localhost:5288/api/Doctors/DeleteDoctorByUsername?airlineUn=${username}`,
      method: "DELETE",
      success: function (response) {
        loadDoctors();
      },
      error: function (error) {
        console.error(error);
      },
    });
  }

  function editDoctor(username) {
    
    $.ajax({
      url: `http://localhost:5288/api/Doctors/GetDoctorByUsername?doctorUn=${username}`,
      method: "GET",
      success: function (response) {
        $doctorName.val(response.name);
        $doctorUsername.val(response.username);
        $doctorClinic.val(response.clinic);
        $doctorPass.val(response.password).addClass("d-none");
        $addBtn.addClass("d-none");
        $updateBtn.removeClass("d-none");
      },
      error: function (error) {
        console.error(error);
      },
    });
  }

  function saveDoctor(event) {
    event.preventDefault();
  
    const doctorName = $doctorName.val().trim();
    const doctorPassword=$doctorPass.val();
    const doctorClinic=$doctorClinic.val();
    const username=$doctorUsername.val();

    if ($doctorUsernameInput.val()) {
      
    


      $.ajax({
        url: `http://localhost:5288/api/Doctors/UpdateDoctorByUsername?doctorUn=${username}`,
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify({ username, name: doctorName, clinic: doctorClinic }),
        success: function (response) {
          loadDoctors();
        },
        error: function (error) {
          console.error(error);
        },
      });
    } else {
      
  
      $.ajax({
        url: "http://localhost:5288/api/Doctor/CreateDoctor",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ name: doctorName, username:username, password:doctorPassword}),
        success: function (response) {
          loadDoctors();
        },
        error: function (error) {
          console.error(error);
        },
      });
    }
    resetForm();
  }

  $doctorForm.on("submit", saveDoctor);

  $doctorsList.on("click", ".edit-btn", function (){
    const username=$(this).data("username");
    editDoctor(username);
  })

  $doctorsList.on("click", ".remove-btn", function () {
    const username = $(this).data("username");
    deleteDoctor(username);
  });

  $updateBtn.on("click", saveDoctor);
    



