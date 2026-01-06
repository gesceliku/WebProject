$firstName=$("#firstName")
$lastName=$("#lastName")
$username=$("#username")
$password=$("#password")
$email=$("#email")
$submit=$("#submit")
$signupForm=$("#signupForm")



var jq = jQuery.noConflict();
jq.ajax({
  url: "https://jsonplaceholder.typicode.com/posts",
  method: "GET",
  success: function (data) {
    console.log(data);
  },
  error: function(error) {
    console.error("GET request error:", error);
  }
});


function createClient(event){
    event.preventDefault();
    const Fullname= $firstName.val().trim()+" "+$lastName.val().trim();
    const username=$username.val();
    const password=$password.val();
    const email=$email.val();

    jq.ajax({
        url: "http://localhost:5288/api/Clients/CreateClient",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ Fullname: Fullname, username: username, password: password, email: email  }),
        success: function (response) {
          console.log("Signed up succesfully", response);
        },
        error: function (error) {
          console.error(error);
        },
      });
      resetForm()

    }
function resetForm(){
        $firstName.val("");
        $lastName.val("");
        $username.val("");
        $password.val("");
        
      }
$signupForm.on("submit", createClient);
      


