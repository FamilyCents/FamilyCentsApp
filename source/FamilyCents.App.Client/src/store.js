import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

const apiUrl = "https://familycents.azurewebsites.net/api";

export const store = new Vuex.Store({
  state: {
    accountId: 0,
    family: [],
    currentUser: {},
    tasks: [],
    isLoading: false
  },
  getters: {
    accountId(state){
      return state.accountId;
    },
    family(state){
      return state.family;
    },
    currentUser(state){
      return state.currentUser;
    },
    tasks(state){
      return state.tasks;
    },
    isLoading(state){
      return state.isLoading;
    }
  },
  mutations: {
    updateFamilyMember(state, memberIn) {     
      let member = state.family.find((m)=>m.customerId === memberIn.customerId);
      
      if(member){
        let newFamily = state.family.filter(m => m.customerId != member.customerId);
        newFamily.push(memberIn);
        state.family = newFamily;
      }
      else
      {
        state.family.push(memberIn);
      }
    },
    updateFamilyTask(state, taskIn) {     
      let task = state.tasks.find(t=>t.id === taskIn.id);
      
      if(task){
        let newTasks = state.tasks.filter(t => t.id != task.id);
        
        //make new Task with updated props from taskIn
        let newTask = {
          ...task,
          ...taskIn
        }

        newTasks.push(newTask);
        state.tasks = newTasks;
      }
      else
      {
        state.tasks.push(taskIn);
      }
    },
    updateCurrentUser(state, currentCustomerId){
      state.currentUser = state.family.find((m)=>m.customerId === currentCustomerId);
    },
    updateAccountId(state, accountId){
      state.accountId = accountId;
    },
    loading(state, isLoading){
      state.isLoading = isLoading;
    }
  },
  actions: {
    reloadFamily(context){
      context.dispatch('loadFamily',[context.getters.accountId, context.getters.currentUser.customerId]);
    },
    initializeFamily(context, [accountId, userId]){
      context.commit('loading', true);
      context.dispatch('loadFamily',[accountId, userId]).then(()=>{
        context.commit('loading', false);
      });
    },
    loadFamily(context, [accountId, userId]){
      context.commit('updateAccountId', accountId);
      let url = `${apiUrl}/family/${accountId}`;
      return fetch(url)
      .then(res => res.json())
      .then(family => {
          for (let member of family) context.commit('updateFamilyMember', member);
          
          context.dispatch('loadTasksPromise').then(() => {
            context.commit('updateCurrentUser', userId);
          });
          
      });
    },
    loadTasksPromise(context) {
      let url = `${apiUrl}/tasks/${context.getters.accountId}`;
      console.log(`LoadTasks url: ${url}`);
      fetch(url)
      .then(res => res.json())
      .then(tasks => {
          for (let task of tasks) context.commit('updateFamilyTask', task);
      });
    },
    approveTask(context, taskId){
      let currentUser = context.getters.currentUser;
      if(currentUser.isPrimary){
        context.dispatch('updateTask', [taskId, {approvedBy: currentUser.customerId}]);
      }
    },
    completeTask(context, taskId){
      let currentUserId = context.getters.currentUser.customerId;
      context.dispatch('updateTask', [taskId, {completedBy: currentUserId}]);
    },
    updateTask(context, [taskId, updateTaskBody]){
      let url = `${apiUrl}/tasks/${context.getters.accountId}/task/${taskId}`;
      fetch(url, 
        {
          method: "PUT",
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(updateTaskBody)
      })
      .then(res => {
          context.dispatch('reloadFamily');
      });
    },

    createFamilyTask(context, taskIn){
      let url = `${apiUrl}/tasks/${context.getters.accountId}/task`;
      fetch(url, 
        {
          method: "POST",
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify({ 
            createdBy: context.getters.currentUser.customerId,
            description: taskIn.description,
            value: taskIn.value
           })
      })
      .then(res => {
          context.dispatch('reloadFamily');
      });
    }
  }
});