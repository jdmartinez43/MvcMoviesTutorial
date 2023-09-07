using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models;

/* This model will contain:
- a list of movies
- a SelectList attribute containing the list of genres, allowing users to select a genre from a list
- a MovieGenre attribute containing the selected genre
- a SearchString attribute containing the text users enter in the search text box
*/
public class MovieGenreViewModel
{
    public List<Movie>? Movies {get; set;}
    public SelectList? Genres { get; set;}
    public string? MovieGenre {get; set;}
    public string? SearchString {get; set;}
}