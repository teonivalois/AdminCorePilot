<template>
    <section class="content">
        <div class="row center-block">
            <div class="col-md-12">
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title" v-if="title">{{title}}</h3>
                        <div class="box-tools pull-right">
                            <div class="input-group input-group-sm" style="width:200px">
                                <input type="text" class="form-control" @keyup.enter="goToPage(1)" v-model="search" :placeholder="$lang.datatable.search">
                                <span class="input-group-btn">
                                    <button class="btn btn-default" @click="goToPage(1)"><i class="fa fa-search"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row data-table-toolbox">
                            <div class="col-md-6">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-default"><i class="fa fa-plus"></i> {{$lang.datatable.add}}</button>
                                </div>
                            </div>
                            <div class="col-md-6 pull-right">
                                <paginate
                                    :page-count="Math.ceil(this.totalItems / this.pageSize)"
                                    :click-handler="goToPage"
                                    :prev-text="''"
                                    :next-text="''"
                                    :prev-link-class="'fa fa-angle-left'"
                                    :next-link-class="'fa fa-angle-right'"
                                    :container-class="'pagination pagination-sm no-margin pull-right'">
                                </paginate>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-striped table-bordered" v-model="items">
                                    <thead>
                                        <tr>
                                            <th v-for="col in columns" @click="sortData(col)">
                                                {{col.title}}
                                                <span v-if="col.name == sortColumn && sortDirection == 'asc'">&uarr;</span>
                                                <span v-if="col.name == sortColumn && sortDirection == 'desc'">&darr;</span>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="item in items">
                                            <td v-for="col in columns">{{item[col.name]}}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer clearfix">
                        
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
  import api from '../api'
  import Paginate from 'vuejs-paginate'

  export default {
    name: 'DataTable',
    data () {
      return {
        currentPage: 0,
        totalItems: 0,
        sortColumn: '',
        sortDirection: '',
        search: '',
        searchColumn: '',
        items: []
      }
    },
    props: ['title', 'url', 'columns', 'pageSize'],
    computed: {
    },
    methods: {
      fetchData () {
        api.request('get', this.url + '?page=' + this.currentPage + '&pageSize=' + this.pageSize + '&orderBy=' + this.sortColumn + '&orderByAscending=' + (this.sortDirection === 'asc') + '&searchColumn=' + encodeURIComponent(this.searchColumn) + '&search=' + encodeURIComponent(this.search), null)
          .then(response => {
            var responseData = response.data
            this.items = responseData.data
            this.totalItems = responseData.count
          })
          .catch(error => {
            console.log(error)
          })
      },
      sortData (column) {
        this.sortDirection = this.sortColumn === column.name && this.sortDirection === 'asc' ? 'desc' : 'asc'
        this.sortColumn = column.name
        this.fetchData()
      },
      goToPage (page) {
        this.currentPage = page - 1
        this.fetchData()
      }
    },
    components: {
      'paginate': Paginate
    },
    mounted () {
      this.goToPage(1)
    }
  }
</script>

<style lang="css">
    .data-table-toolbox {
        padding-bottom: 10px;
        padding-top: 10px;
    }
</style>
