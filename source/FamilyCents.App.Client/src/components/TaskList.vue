<template>
  <div class="task-list">
    <!-- Main content -->
    <section>
        <div>
        <v-list two-line subheader>
          <v-subheader>Tasks</v-subheader>
          <v-list-tile v-for="(task, index) in taskList" :key="index">
            <v-list-tile-action>
              <v-checkbox 
              v-model="task.completedBy"
              @click="completeTask(task.id)"
              ></v-checkbox>
              <v-checkbox 
              v-model="task.approvedBy"
              @click="approveTask(task.id)"
              v-if="isParent"></v-checkbox>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>{{task.description}} - ${{task.value}}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
        </div>
    </section>
  </div>
</template>

<script>


export default {
  name: 'TaskList',
  computed: {
    taskList() {
      console.log(`task list: ${this.$store.getters.tasks}`);
      return this.$store.getters.tasks.filter(t => !(t.approvedBy && t.completedBy));
    },
    isParent: function(){
      let currentUser = this.$store.getters.currentUser;      
      if(currentUser){
        console.log(`Is Parent called: ${this.$store.getters.currentUser.isPrimary}`);
        return this.$store.getters.currentUser.isPrimary;
      }
      return false;
    }
  },
  methods: {
    completeTask(taskId){
      // let taskId = event.target.value;
      console.log(`TaskId: `+taskId);
      this.$store.dispatch('completeTask', taskId);
    },
    approveTask(taskId){
      console.log(`TaskId: `+taskId);
      this.$store.dispatch('approveTask', taskId);
    }
  }
};

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
