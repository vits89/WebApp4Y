﻿using Nancy;
using Nancy.Testing;
using Xunit;
using WebApp4Y.Helpers;
using WebApp4Y.Modules;
using WebApp4Y.ViewModels;
using WebApp4Y.Tests.Helpers;

namespace WebApp4Y.Tests
{
    public class ListModuleTests
    {
        private readonly Browser _browser;

        public ListModuleTests()
        {
            ITopStoriesApiHelper fakeTopStoriesApiHelper = new FakeTopStoriesApiHelper();

            _browser = new Browser(c =>
            {
                c.Module<ListModule>();
                c.Dependency(fakeTopStoriesApiHelper);
            }, c => c.Accept("application/json"));
        }

        [Fact]
        public async void Test1()
        {
            var response = await _browser.Get("/list/home");

            var articles = response.Body.DeserializeJson<ArticleView[]>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(3, articles.Length);
        }

        [Fact]
        public async void Test2()
        {
            var response = await _browser.Get("/list/home/first");

            var article = response.Body.DeserializeJson<ArticleView>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal("Heading 1", article.Heading);
        }

        [Fact]
        public async void Test3()
        {
            var response = await _browser.Get("/list/home/2019-05-17");

            var articles = response.Body.DeserializeJson<ArticleView[]>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(2, articles.Length);
        }
    }
}
