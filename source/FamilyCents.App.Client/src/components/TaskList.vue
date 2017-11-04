<template>
  <div class="task-list">
    <ul>
      <li v-for="(task, index) in taskList" :key="index">
        <div> Task: {{task.description}}</div>
        <div> Reward: {{task.reward}}</div>        
      </li>
    </ul>
  </div>
</template>

<script>


export default {
  name: 'TaskList',
  computed: {
    taskList() {
      console.log(taskListSample);
      return taskListSample;
    }
  },
  created(){
    // this.$store.dispatch('connectRoom', 'sample');
  }
};

const taskListSample = [
  {
    id: 0,
    description: "This is a task",
    reward: 5
  },
  {
    id: 0,
    description: "This is a task",
    reward: 5
  },
  {
    id: 1,
    description: "This is a task",
    reward: 5
  },
  {
    id: 2,
    description: "This is a task",
    reward: 5
  },
  {
    id: 3,
    description: "This is a task",
    reward: 5
  }
]


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
