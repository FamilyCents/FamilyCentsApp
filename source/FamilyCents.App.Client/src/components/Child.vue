<template>
  <div class="child">
    <h1>The child's view</h1>
    <TaskList></TaskList>
  </div>
</template>

<script>

import TaskList from './TaskList';

export default {
  name: 'Child',
  components: {
    TaskList
  },
  data () {
    return {
      name: 'Charlie Brown'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
