<template>
  <div class="parent">
    <h1>The parent's view</h1>
    <TaskList></TaskList>
  </div>
</template>

<script>

import TaskList from './TaskList';

export default {
  name: 'Parent',
  components: {
    TaskList
  },
  data () {
    return {
      name: 'John Johnson'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>