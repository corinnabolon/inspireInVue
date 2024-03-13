<template>
  <!-- <span class="navbar-text"> -->
  <span>
    <button class="btn selectable text-success lighten-30 fredoka-font my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="my-2 my-lg-0">
        <div type="button" class="selectable no-select">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="40" class="rounded" />
          </div>
        </div>
      </div>
      <!-- <div class="dropup-menu dropup-menu-lg-end dropup-menu-start p-0" aria-labelledby="authDropdown">
        <div class="list-group"> -->
      <!-- <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropup-item list-group-item-action">
                Manage Account
              </div>
            </router-link> -->
      <!-- <div class="list-group-item dropup-item list-group-item-action text-danger selectable" @click="logout">
        <i class="mdi mdi-logout"></i>
        logout
      </div> -->
      <!-- </div>
      </div> -->
      <!-- //TODO: fix this placement -->
      <div
        class="list-group-item dropup-item list-group-item-action text-danger selectable text-bg text-center fredoka-font mt-3 rounded"
        @click="logout">
        <i class="mdi mdi-logout"></i>
        logout
      </div>
    </div>
    <!-- </div> -->
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>
