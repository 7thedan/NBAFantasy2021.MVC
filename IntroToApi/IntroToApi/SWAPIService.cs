﻿using IntroToApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToApi
{
    class SWAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<Person> GetPersonAsync(string url)

            //Async Method
        {
            ////Get Request
            //HttpResponseMessage response = await _httpClient.GetAsync(url);

            //if (response.IsSuccessStatusCode)
            //{
            //    //Was a success
            //    Person person = await response.Content.ReadAsAsync<Person>();
            //    return person;
            //}
            ////Was not a success
            //return null;

            return await GetAsync<Person>(url);
        }

        public async Task<Vehicle> GetVehicleAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            return response.IsSuccessStatusCode 
                ? await response.Content.ReadAsAsync<Vehicle>() 
                : null;
        }

        public async Task<T> GetAsync<T>(string url) where T:class //where T makes it return null.
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                T content = await response.Content.ReadAsAsync<T>();
                return content; 
            }

            return null;
        }

        public async Task<SearchResult<Person>> GetPersonSearchAsync(string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://swapi.dev/api/people?search=" + query);

            if(response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<SearchResult<Person>>();

            return null;
        }

        public async Task<SearchResult<T>> GetSearchAsync<T>(string query, string category)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://swapi.dev/api/{category}?search={query}");

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<SearchResult<T>>()
                : default;
        }

        public async Task<SearchResult<Vehicle>> GetVehicleSearchAsync(string query)
        {
            return await GetSearchAsync<Vehicle>(query, "vehicles");
        }
    }
}
