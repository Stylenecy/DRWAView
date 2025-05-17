using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;
using SimpleRESTAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection 
builder.Services.AddScoped<IInstructor, InstructorADO>();
builder.Services.AddSingleton<ICategory, CategoryADO>();
builder.Services.AddScoped<ICourses, CourseEF>();

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
app.Run();