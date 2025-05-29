using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;
using SimpleRESTAPI.Data;
using Simple_API.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection 
builder.Services.AddScoped<IInstructor, InstructorADO>();
builder.Services.AddSingleton<ICategory, CategoryADO>();
builder.Services.AddScoped<ICourses, CourseEF>();
builder.Services.AddScoped<IAspUser, AspUserEF>();

//AutoMapper
// builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Categories API
app.MapGet("api/v1/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});
app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategory(id);
    return category;
});
app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});
app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var updateCategory = categoryData.UpdateCategory(category);
    return updateCategory;
});
app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

app.MapGet("api/v1/instructor", (IInstructor instructorData) =>
{
    var instructor = instructorData.GetInstructors();
    return instructor;
});
app.MapGet("api/v1/instructor/{id}", (IInstructor instructorData, int id) =>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor;
});
app.MapPost("api/v1/instructor", (IInstructor instructorData, Instructor instructor) =>
{
    var newInstructor = instructorData.AddInstructor(instructor);
    return newInstructor;
});
app.MapPut("api/v1/instructor", (IInstructor instructorData, Instructor instructor) =>
{
    var updateInstructor = instructorData.UpdateInstructor(instructor);
    return updateInstructor;
});
app.MapDelete("api/v1/instructor/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

app.MapGet("api/v1/courses", (ICourses courseData) =>
{
    List<CourseDTO> courseDTOs = new List<CourseDTO>();
    var courses = courseData.GetAllCourses();
    //mapping to CourseDTO
    foreach (var course in courses)
    {
        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            CourseDescription = course.CourseDescription,
            Duration = course.Duration,
            Category = new CategoryDTO
            {
                categoryId = course.Category.CategoryId,
                categoryName = course.Category.CategoryName
            },
            Instructor = course.Instructor != null ? new InstructorDTO
            {
                InstructorId = course.Instructor.InstructorId,
                InstructorName = course.Instructor.InstructorName
            }
            : null

        };
        courseDTOs.Add(courseDTO);
    }
    return courseDTOs;
});
app.MapGet("api/v1/courses/{id}", (ICourses courseData, int id) =>
{
    CourseDTO courseDTO = new CourseDTO();
    var course = courseData.GetCourseByIdCourse(id);
    if (course == null)
    {
        return Results.NotFound();
    }
    courseDTO.CourseId = course.CourseId;
    courseDTO.CourseName = course.CourseName;
    courseDTO.CourseDescription = course.CourseDescription;
    courseDTO.Duration = course.Duration;
    courseDTO.Category = new CategoryDTO
    {
        categoryId = course.Category.CategoryId,
        categoryName = course.Category.CategoryName
    };
    courseDTO.Instructor = new InstructorDTO
    {
        InstructorId = course.Instructor.InstructorId,
        InstructorName = course.Instructor.InstructorName
    };
    return Results.Ok(courseDTO);
});
app.MapPost("api/v1/courses", (ICourses courseData, AddCourseDTO courseAddDto) =>
{
    try
    {
        Course course = new Course
        {
            CourseName = courseAddDto.CourseName,
            CourseDescription = courseAddDto.CourseDescription,
            Duration = courseAddDto.Duration,
            CategoryId = courseAddDto.categoryId,
            InstructorId = courseAddDto.InstructorId
        };

        var newCourse = courseData.AddCourse(course);

        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = newCourse.CourseId,
            CourseName = newCourse.CourseName,
            CourseDescription = newCourse.CourseDescription,
            Duration = newCourse.Duration,
            Category = newCourse.Category != null ? new CategoryDTO
            {
                categoryId = newCourse.Category.CategoryId,
                categoryName = newCourse.Category.CategoryName
            } : null,
            Instructor = newCourse.Instructor != null ? new InstructorDTO
            {
                InstructorId = newCourse.Instructor.InstructorId,
                InstructorName = newCourse.Instructor.InstructorName
            } : null
        };

        return Results.Created($"/api/v1/courses/{courseDTO.CourseId}", courseDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("api/v1/courses", (ICourses courseData, Course course) =>
{
    try
    {
        var updatedCourse = courseData.UpdateCourse(course);

        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = updatedCourse.CourseId,
            CourseName = updatedCourse.CourseName,
            CourseDescription = updatedCourse.CourseDescription,
            Duration = updatedCourse.Duration,
            Category = new CategoryDTO
            {
                categoryId = updatedCourse.CategoryId,
                categoryName = updatedCourse.Category.CategoryName
            },
            Instructor = new InstructorDTO
            {
                InstructorId = updatedCourse.InstructorId,
                InstructorName = updatedCourse.Instructor.InstructorName
            }
        };

        return Results.Ok(courseDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("api/v1/cekpassword/{password}", (string password) =>
{
    var pass = SimpleRESTApi.Helpers.HashHelper.HashPassword(password);
    // Implement your password checking logic here
    return Results.Ok($"Password is valid: {pass}");
});

// register
app.MapGet("api/v1/aspUsers", (IAspUser aspUserData) =>
{
    var users = aspUserData.GetAllUsers();
    return Results.Ok(users);
});

app.MapGet("api/v1/aspUsers/{username}", (IAspUser aspUserData, string username) =>
{
    var user = aspUserData.GetUserByUsername(username);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

app.MapPost("api/v1/aspUsers", (IAspUser aspUserData, AspUser user) =>
{
    if (user == null)
    {
        return Results.BadRequest("User data cannot be null");
    }

    var registerDto = new RegisterDTO
    {
        Username = user.Username,
        Password = user.Password,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        Firstname = user.FirstName,
        Lastname = user.LastName,
        Address = user.Address,
        City = user.City,
        Country = user.Country
    };
    var newUser = aspUserData.RegisterUser(registerDto);
    return Results.Created($"/api/v1/aspUsers/{newUser.Username}", newUser);
});

app.MapPut("api/v1/aspUsers", (IAspUser aspUserData, AspUser user) =>
{
    if (user == null)
    {
        return Results.BadRequest("User data cannot be null");
    }

    try
    {
        var updatedUser = aspUserData.UpdateUser(user);
        return Results.Ok(updatedUser);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("api/v1/aspUsers/{username}", (IAspUser aspUserData, string username) =>
{
    try
    {
        var deleteUser = aspUserData.DeleteUser(username);
        return Results.Ok(deletedUser);
    }
    catch (KeyNotFoundException knfEx)
    {
        return Results.NotFound(knfEx.Message);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("api/v1/login", (IAspUser aspUserData, LoginDTO dto) =>
{
    if (dto == null)
    {
        return Results.BadRequest("Login data cannot be null");
    }

    bool isAuthenticated = aspUserData.Login(dto);

    if (!isAuthenticated)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new { message = "Login successful ", username = dto.Username });
});

app.Run();