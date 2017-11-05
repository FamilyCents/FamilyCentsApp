<template>
  <div class="task-list">
    <!-- Main content -->
    <section>
        <div>
        <v-list two-line subheader>
          <v-subheader>Tasks</v-subheader>
          <v-list-tile avatar>
            <v-list-tile-action>
              <v-checkbox v-model="notifications"></v-checkbox>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>Notifications</v-list-tile-title>
              <v-list-tile-sub-title>Allow notifications</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
            <div headline>
              Tasks
            </div>
            <div>
                <ul>
                  <li v-for="(task, index) in taskList" :key="index">
                    <div> Task: {{task.description}}</div>
                    <div> Reward: {{task.reward}}</div>        
                  </li>
                </ul>
            </div>
        </div>
    </section>
    <!-- /.content -->
  </div>
</template>

<script>


export default {
  name: 'TaskList',
  data() {
    return {
      taskList: taskListSample
    }
  },
  computed: {
    taskList() {
      // console.log(taskListSample);
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
