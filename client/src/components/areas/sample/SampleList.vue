<template>
    <div>
        <data-table :title="title" :url="url" :columns="columns" :pageSize="10" />
    </div>
</template>

<script>
import DataTable from '../../DataTable.vue'
export default {
  name: 'SampleList',
  data () {
    return {
      title: 'A simple data sample',
      url: '/sample',
      columns: [{name: 'title', title: 'Title'}, {name: 'body', title: 'Body'}]
    }
  },
  components: {
    'data-table': DataTable
  }
}
</script>

<style lang="scss">
    
</style>
