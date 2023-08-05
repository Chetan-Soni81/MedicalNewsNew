var app = angular.module("MyApp", ['ui.bootstrap'])

app.controller("LinksController", ($scope, $http) => {
    $scope.links = [];
    $scope.totalpage = 1;
    $scope.currentpage = 1;
    $scope.maxsize = 10;

    $http.get("/Admin/Home/GetLinkJson?pageno=" + $scope.currentpage).then((res) => {
        $scope.currentpage = res.data.pageNo;
        $scope.totalpage = res.data.totalPage;

        for (var i = 0; i < res.data.links.length; i++) {
            $scope.links.push(res.data.links[i]);
            console.log(res.data.links[i]);
        }
    }, (res) => {
        console.log(res)
    });

    $scope.$watch('currenpage',() => {
        $http.get("/Admin/Home/GetLinkJson?pageno="+$scope.currentpage).then((res) => {
            $scope.currentpage = res.data.pageNo;
            $scope.totalpage = res.data.totalPage;
            $scope.links = []
            for (var i = 0; i < res.data.links.length; i++) {
                $scope.links.push(res.data.links[i]);
                console.log(res.data.links[i]);
            }
        }, (res) => {
            console.log(res)
        });
    });
})

app.controller("ArticleController", ($scope, $http) => {

    $scope.articlecategory = [];

    $http.get('/Category/GetArticleJson').then((res) => {

        for (var i = 0; i < res.data.length; i++) {
            $scope.articlecategory.push(res.data[i]);
        }
    }, (res) => {
        console.log(res)
    })



    var CreateArticleCategory = (valid) => {
        if (!valid) {

        } else {

        }
    }

});