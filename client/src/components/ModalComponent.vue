<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="mb-0 pb-0 mt-2 ms-2 d-flex align-items-center justify-content-between" :id="modalId + 'Label'">

          <slot name="modalTitle"></slot>

          <button type="button" class="btn-close mb-0 pb-0 me-2 fs-5" data-bs-dismiss="modal"
            aria-label="Close"></button>
        </div>
        <div class="mt-0">

          <slot name="modalBody"></slot>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    modalId: { type: String, required: true },
    modalSize: { type: String, default: '' }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped></style>